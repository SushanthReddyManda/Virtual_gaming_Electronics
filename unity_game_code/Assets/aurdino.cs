using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO.Ports;


public class aurdino : MonoBehaviour
{

    SerialPort stream;
    public float sidewaysforce = 120f;
    public Rigidbody rb;
    public Rigidbody cam;
    public int a;
    public int b;

    public GameObject target; // is the gameobject to

    float acc_normalizer_factor = 0.00025f;
    float gyro_normalizer_factor = 1.0f / 32768.0f;   // 32768 is max value captured during test on imu

    float curr_angle_x = 0;
    float curr_angle_y = 0;
    float curr_angle_z = 0;

    float curr_offset_x = 0;
    float curr_offset_y = 0;
    float curr_offset_z = 5;

    // Increase the speed/influence rotation
    public float factor = 7;

   
    public bool enableRotation;
    public bool enableTranslation;

    // SELECT YOUR COM PORT AND BAUDRATE
    string port = "COM15";
    int baudrate = 9600;
    int readTimeout = 10;

    void Start()
    {
        // open port. Be shure in unity edit > project settings > player is NET2.0 and not NET2.0Subset
        stream = new SerialPort("\\\\.\\" + port, baudrate);

        try
        {
            stream.ReadTimeout = readTimeout;
        }
        catch (System.IO.IOException ioe)
        {
            Debug.Log("IOException: " + ioe.Message);
        }
        

        stream.Open();
        
    }

    void Update()
    {
        string dataString = "null received";

        if (stream.IsOpen)
        {
            try
            {
                dataString = stream.ReadLine();
                Debug.Log("RCV_ : " + dataString);
            }
            catch (System.IO.IOException ioe)
            {
                Debug.Log("IOException: " + ioe.Message);
            }

        }
        else
            dataString = "NOT OPEN";
        Debug.Log("RCV_ : " + dataString);

        if (!dataString.Equals("NOT OPEN"))
        {
            // recived string is  like  "accx;accy;accz;gyrox;gyroy;gyroz"
            char splitChar = ';';
            string[] dataRaw = dataString.Split(splitChar);

            // normalized accelerometer values
            float ax = Int32.Parse(dataRaw[0]) * acc_normalizer_factor;
            float ay = Int32.Parse(dataRaw[1]) * acc_normalizer_factor;
            float az = Int32.Parse(dataRaw[2]) * acc_normalizer_factor;

            // normalized gyrocope values
            float gx = Int32.Parse(dataRaw[3]) * gyro_normalizer_factor;
            float gy = Int32.Parse(dataRaw[4]) * gyro_normalizer_factor;
            float gz = Int32.Parse(dataRaw[5]) * gyro_normalizer_factor;
            float a = Int32.Parse(dataRaw[6]);
            float b = Int32.Parse(dataRaw[7]);

            // prevent
            if (Mathf.Abs(ax) - 1 < 0) ax = 0;
            if (Mathf.Abs(ay) - 1 < 0) ay = 0;
            if (Mathf.Abs(az) - 1 < 0) az = 0;


            // The IMU module have value of z axis of 16600 caused by gravity


            // prevent little noise effect
            if (Mathf.Abs(gx) < 0.025f) gx = 0f;
            if (Mathf.Abs(gy) < 0.025f) gy = 0f;
            if (Mathf.Abs(gz) < 0.025f) gz = 0f;

            curr_angle_x += gx;
            curr_angle_y += gy;
            curr_angle_z += 0;

            if (140>a&&a>125 && 140>b&&b>125)
            {
                if (enableTranslation)
                {
                    target.transform.position = new Vector3(-curr_offset_x,1,- curr_offset_y);
                    curr_offset_x += ax;
                    curr_offset_y += ay;
                    
                    
                }
            }
            if(80<a&&a<110 && 80< b&&b<110)
            {
                if (enableRotation)
                {
                    target.transform.rotation = Quaternion.Euler(0, -curr_angle_z * factor,- curr_angle_y * factor);
                    ax = 0;
                }
            }
            /*
            if (112<a&&a < 120 && 112<b&b <120)
            {
                if (enableTranslation)
                {
                    cam.transform.rotation= Quaternion.Euler(0, -curr_angle_z * factor, -curr_angle_y * factor);
                    ax = 0;
                }
            }
            */
            
            if (160>a&&a >140 &&160> b&&b >140)
            {
                if (enableRotation)
                {
                    
                    ax = 0;
                }
            }


        }

    }

  
}
