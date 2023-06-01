using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class ScubaDive2 : MonoBehaviour
{
    public Image AirBar;
    public Image FinBar;
    public Gradient gradient;
    public AudioClip audio2;
    public AudioClip audio3;
    public AudioSource audioSource;
    public bool isPumping;
    public bool isDeflating;
    public float pumpStartTime;
    public float deflateStartTime;
    public float pumpInterval;
    public float depth;
    public float INIT_depth;
    public float velocity1;
    public float velocity;
    public float INIT_velocity;
    public float accelaration;
    public float net_force;
    public float mass;
    public float air;
    public float max_air;
    public float INIT_air;
    public float delayed_inflow2;
    public float inflow_air;

    public float delayed_outflow2;
    public float outflow_air;
    public float into;
    public float INIT_into;
    public float dt;


    public float delayed_inflow1;
    public float inflow_delay_time;
    public float out_to;
    public float INIT_out_to;

    public float delayed_outflow1;

    public float perceived_depth;
    public float INIT_perceived_depth;
    public float Noname_2;
    public float score;
    public float deviation;
    public float INIT_score;

    public float Noname_4;


    public float cross_sectional_area;
    public float density;
    public float denstity_of_a_human_body;
    public float depth_perception_time;
    public float desired_height;
    public float discrepancy;
    public float disturbance;
    public float effective_depth;
    public float flow_1;
    public float flow_3;
    public float frictional_force;
    public float frictional_force_vector;

    public float gravitational_constant;
    public float height_of_the_man;


    public float lifting_force;
    public float lt_m3_donusumu;


    public float minus_velocity;
    public float normal_flow;
    public float change_in_air;

   
    public float Delay_05_sec;
    public float No_Delay;
    public float pressure;
    public float radius;
    public float radius_of_the_man;
    public float RT;
    public float volume_in_water;

    public float vol_bal_in_water;
    public float vol_man_in_water;
    public float volume_lt;
    public float volume_m3;
    public float volume_of_the_man;

    public float weight;
    public float Your_depth;
    public float You_are_at_10_mt_but_there_is_a_disturbance_Stabilize_at_10_mt;
    public float You_are_at_20_mt_depth__Stabilize_at_10_mt;
    public float You_are_at_the_surface__Stabilize_at_10_mt;
    public float t;
    public float Fin_decision;
    public float Fin_time;
    public float Air_Adjustment_Decision;
    public TextMeshProUGUI messageText;

    public bool gameStarted;
    public float updateInterval = 0.0f;
    public float elapsedTime = 0.0f;
    public Button buttonNoDelay;
    public Button buttonDelay;
    public Button buttonFastFlow;
    public Button buttonSlowFlow;
    public Button buttonStart;

    public float Pulse(float air_value, float t_value)
    {
        if (t_value < 5.01f && t_value > 4.99f) { return air_value; }

        else { return 0; }
    }
    // Start is called before the first frame update
    void Start()
    {

        AirBar = GameObject.Find("Air Bar").GetComponent<Image>();
        FinBar = GameObject.Find("Fin Bar").GetComponent<Image>();

        isPumping = false;
        isDeflating = false;
        pumpInterval = 1.5f;
        buttonDelay = GameObject.Find("Delay").GetComponent<Button>();
        buttonNoDelay = GameObject.Find("NoDelay").GetComponent<Button>();
        buttonFastFlow = GameObject.Find("fast flow").GetComponent<Button>();
        buttonSlowFlow = GameObject.Find("slow flow").GetComponent<Button>();
        buttonStart = GameObject.Find("start").GetComponent<Button>();
        //initalization equations
        dt = 0.01f;
        deviation = 0;

        t = 0;

        Fin_time = 0;
        change_in_air = 0;
        gameStarted = false;
        into = 0;
        //initial konum seçme kodu yaz sadece bir kez input alýnacak
        You_are_at_the_surface__Stabilize_at_10_mt = 0;
        You_are_at_20_mt_depth__Stabilize_at_10_mt = 0;
        You_are_at_10_mt_but_there_is_a_disturbance_Stabilize_at_10_mt = 1;
        depth = You_are_at_10_mt_but_there_is_a_disturbance_Stabilize_at_10_mt * 10 + You_are_at_20_mt_depth__Stabilize_at_10_mt * 20 + You_are_at_the_surface__Stabilize_at_10_mt * 0;

        //effective depth
        if (depth < -0.5) { effective_depth = 0; }
        else if (depth < 0) { effective_depth = (0.5f + depth) / 2f; }
        else { effective_depth = depth + 0.5f / 2f; }

        pressure = 1 + effective_depth / 10;

        // flow 1 ve 3 için flow decision kodu yap------------------------------------- sadece bir kez input alýnacak
        flow_3 = 1;
        flow_1 = 0;
        //flow 1 ve 3 için flow decision kodu yap-----------------------------------------------

        normal_flow = flow_1 * 1 + flow_3 * 3;
        RT = 25;
        Air_Adjustment_Decision = 0; //onuser update de air adjustment kodu yazDIN input alýncak
        Fin_decision = 0;
        if (Air_Adjustment_Decision == 1) { inflow_air = normal_flow * Air_Adjustment_Decision * pressure / RT; }
        else { inflow_air = 0; }
        inflow_delay_time = 0.5f;
        delayed_inflow1 = into / inflow_delay_time;

        No_Delay = 1;
        Delay_05_sec = 0;
        
        delayed_inflow2 = inflow_air * (1 - (Delay_05_sec)) + delayed_inflow1 * (Delay_05_sec);

        if (Air_Adjustment_Decision == -1) { outflow_air = normal_flow * Air_Adjustment_Decision * (-1) * pressure / RT; }
        else { outflow_air = 0; }

        mass = 90;
        max_air = 3;
        denstity_of_a_human_body = 1070;
        volume_of_the_man = mass / denstity_of_a_human_body;
        air = pressure * (90 - volume_of_the_man * 1000) / RT;
        out_to = 0;
        delayed_outflow1 = out_to / inflow_delay_time;
        disturbance = You_are_at_10_mt_but_there_is_a_disturbance_Stabilize_at_10_mt;
        delayed_outflow2 = outflow_air * (1 - Delay_05_sec ) + delayed_outflow1 * (Delay_05_sec) + disturbance * Pulse(air / 4, t) * 100;
        perceived_depth = You_are_at_10_mt_but_there_is_a_disturbance_Stabilize_at_10_mt * 10 + You_are_at_20_mt_depth__Stabilize_at_10_mt * 20 + You_are_at_the_surface__Stabilize_at_10_mt * 0;
        depth_perception_time = 0.5f;
        Noname_2 = (depth - perceived_depth) / depth_perception_time;

        Noname_4 = Mathf.Abs(10.0f - depth) + 15.0f * Mathf.Abs(Fin_decision);
        velocity = 0;
        score = 0;
        density = 1000;
        gravitational_constant = 9.81f;

        if (depth < -1.5) { vol_man_in_water = 0; }
        else
        {
            if (depth < 0) { vol_man_in_water = volume_of_the_man * (1.5f + depth) / 1.5f; }
            else { vol_man_in_water = volume_of_the_man; }
        }

        volume_lt = Mathf.Min(25, air * RT / pressure);
        lt_m3_donusumu = 1000;
        volume_m3 = volume_lt / lt_m3_donusumu;
        //vol_bal_in_water
        if (depth < -0.5) { vol_bal_in_water = 0; }
        else if (depth < 0) { vol_bal_in_water = volume_m3 * (0.5f + depth) / 0.5f; }
        else { vol_bal_in_water = volume_m3; }

        volume_in_water = vol_bal_in_water + vol_man_in_water;

        lifting_force = -volume_in_water * gravitational_constant * density;
        weight = mass * gravitational_constant;
        frictional_force = 27.2f * Mathf.Pow(velocity, 2f);
        //frictional_force_vector
        if (velocity == 0) { frictional_force_vector = 0; }
        else { frictional_force_vector = -velocity / Mathf.Abs(velocity) * frictional_force; }

        net_force = weight + lifting_force + frictional_force_vector;
        accelaration = net_force / mass;
        velocity1 = velocity;
        height_of_the_man = 1.50f;
        radius_of_the_man = Mathf.Pow((mass / (height_of_the_man * denstity_of_a_human_body * 3.1415f)), (1 / 2));
        radius = Mathf.Pow(((volume_m3 / (3.1415f * (height_of_the_man - 1))) + Mathf.Pow(radius_of_the_man, 2f)), (1 / 2));
        cross_sectional_area = 3.1415f * Mathf.Pow(radius, 2f);
        desired_height = 10;
        discrepancy = desired_height - depth;

        minus_velocity = -velocity;

        Your_depth = depth * (-1) * (1 - (Delay_05_sec)) + perceived_depth * (-1) * (Delay_05_sec);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {


        if (gameStarted)
        {
            dt = Time.deltaTime;

            //elapsedTime += Time.deltaTime;
            if (air <= max_air)
            {
                into = into + (inflow_air - delayed_inflow1) * dt;
            }
            else { into = 0; }

            depth = depth + (velocity1) * dt;
            change_in_air = air + (delayed_inflow2 - delayed_outflow2) * dt;
            if (change_in_air < 0) { air = 0; }
            else { air = Mathf.Min(max_air, change_in_air); }

            out_to = out_to + Mathf.Min(air, (outflow_air - delayed_outflow1)) * dt;
            perceived_depth = perceived_depth + (Noname_2) * dt;
            velocity = velocity + (accelaration) * dt;
            score = score + (Noname_4) * dt;
            deviation = deviation + Mathf.Abs(10 - depth) * dt;
            Fin_time = Fin_time + Mathf.Abs(Fin_decision) * dt;

            if (depth < -0.5) { effective_depth = 0; }
            else if (depth < 0) { effective_depth = (0.5f + depth) / 2f; }
            else { effective_depth = depth + 0.5f / 2f; }

            pressure = 1 + effective_depth / 10;
            normal_flow = flow_1 * 1 + flow_3 * 3;

            //air adjustment decision make

            if (Input.GetKey(KeyCode.X)) {
                isDeflating = false;
                Air_Adjustment_Decision = 1;
                if(isPumping==false)
                {
                    isPumping = true;
                    pumpStartTime = Time.time;
                    if (audio2 != null)
                    {
                        audioSource.PlayOneShot(audio2);
                    }
                }
                else if (Time.time >= pumpStartTime + pumpInterval)
                {
                    pumpStartTime = Time.time;
                    audioSource.PlayOneShot(audio2);
                }
            }
            else if (Input.GetKey(KeyCode.Z)) {
                isPumping = false;
                Air_Adjustment_Decision = -1;
                if(isDeflating==false)
                {
                    isDeflating = true;
                    deflateStartTime = Time.time;
                    if(audio3 != null)
                    {
                        audioSource.PlayOneShot(audio3);
                    }
                }
                else if (Time.time >= deflateStartTime + pumpInterval)
                {
                    deflateStartTime = Time.time;
                    audioSource.PlayOneShot(audio3);
                }


            }
            else {
                audioSource.Stop();
                isPumping = false;
                isDeflating = false;
                Air_Adjustment_Decision = 0;
              
            }

            if (Fin_time < 30f)
            {

                if (Input.GetKey(KeyCode.M)) { Fin_decision = 1; }
                else if (Input.GetKey(KeyCode.N)) { Fin_decision = -1; }
                else { Fin_decision = 0; }
            }
            else
            {
                Fin_decision = 0;
            }

            if (Air_Adjustment_Decision == 1) { inflow_air = normal_flow * Air_Adjustment_Decision * pressure / RT; }
            else { inflow_air = 0; }

            delayed_inflow1 = into / inflow_delay_time;
            delayed_inflow2 = inflow_air * (1 - Delay_05_sec) + delayed_inflow1 * (Delay_05_sec);

            if (Air_Adjustment_Decision == -1) { outflow_air = normal_flow * Air_Adjustment_Decision * (-1) * pressure / RT; }
            else { outflow_air = 0; }

            volume_of_the_man = mass / denstity_of_a_human_body;
            delayed_outflow1 = out_to / inflow_delay_time;
            disturbance = You_are_at_10_mt_but_there_is_a_disturbance_Stabilize_at_10_mt;
            delayed_outflow2 = outflow_air * (1 - Delay_05_sec ) + delayed_outflow1 * (Delay_05_sec) + disturbance * Pulse(air / 4, t) * 100;
            Noname_2 = (depth - perceived_depth) / depth_perception_time;
            Noname_4 = Mathf.Abs(10.0f - depth) + 15.0f * Mathf.Abs(Fin_decision);


            if (depth < -1.5) { vol_man_in_water = 0; }
            else
            {
                if (depth < 0) { vol_man_in_water = volume_of_the_man * (1.5f + depth) / 1.5f; }
                else { vol_man_in_water = volume_of_the_man; }
            }

            volume_lt = Mathf.Min(25, air * RT / pressure);
            volume_m3 = volume_lt / lt_m3_donusumu;

            if (depth < -0.5) { vol_bal_in_water = 0; }
            else if (depth < 0) { vol_bal_in_water = volume_m3 * (0.5f + depth) / 0.5f; }
            else { vol_bal_in_water = volume_m3; }

            volume_in_water = vol_bal_in_water + vol_man_in_water;
            lifting_force = -volume_in_water * gravitational_constant * density;
            weight = mass * gravitational_constant;
            frictional_force = 27.2f * Mathf.Pow(velocity, 2);

            //frictional_force_vector
            if (velocity == 0) { frictional_force_vector = 0; }
            else { frictional_force_vector = -velocity / Mathf.Abs(velocity) * frictional_force; }

            net_force = weight + lifting_force + frictional_force_vector - 35f * Fin_decision;
            accelaration = net_force / mass;
            velocity1 = velocity;
            radius_of_the_man = Mathf.Pow((mass / (height_of_the_man * denstity_of_a_human_body * 3.1415f)), (1 / 2));
            radius = Mathf.Pow(((volume_m3 / (3.1415f * (height_of_the_man - 1))) + Mathf.Pow(radius_of_the_man, 2)), (1 / 2));
            cross_sectional_area = 3.1415f * Mathf.Pow(radius, 2);
            discrepancy = desired_height - depth;
            minus_velocity = -velocity;
            Your_depth = depth * (-1) * (1 - (Delay_05_sec)) + perceived_depth * (-1) * (Delay_05_sec);

            //transform.Translate(Vector2.up * Your_depth);
            //transform.Translate(Vector2.right * t);

            if (depth > 26) { depth = 26f; }

            transform.position = Vector3.down * -(Your_depth + 30) + Vector3.right * 80;

            AirBar.fillAmount = air / max_air;
            
            FinBar.fillAmount = (30f - Fin_time) / 30f;
            FinBar.color = gradient.Evaluate(FinBar.fillAmount);
            //transform.position = Vector2.right * (t-20);
            t += dt;


            if (t >= 120f)
            {
                gameStarted = false;

            }

            // Your code here
            elapsedTime = 0.0f;

        }
        else
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                messageText.text = "Hello player";
            }
            else if (Input.GetKeyDown(KeyCode.K))
            {
                messageText.text = "";
            }

            messageText.text = score.ToString();

            buttonFastFlow.onClick.AddListener(FastFlow);
            buttonSlowFlow.onClick.AddListener(SlowFlow);
            buttonStart.onClick.AddListener(GameStarter);
            buttonNoDelay.onClick.AddListener(NoDelay);
            buttonDelay.onClick.AddListener(Delay);



        }


    }

    
    public void NoDelay()
    {
        No_Delay = 1;
        Delay_05_sec = 0;
    }

    public void Delay()
    {
        No_Delay = 0;
        Delay_05_sec = 1;
    }


    public void FastFlow()
    {
        flow_1 = 0;
        flow_3 = 1;

    }
    public void SlowFlow()
    {

        flow_1 = 1;
        flow_3 = 0;
    }
    public void GameStarter()
    {
        gameStarted = true;
        messageText.text = "";

    }




}