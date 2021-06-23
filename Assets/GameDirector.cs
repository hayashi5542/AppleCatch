using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    GameObject timerText;
    GameObject pointText;
    float time = 30.0f;
    int point = 0;
    GameObject generator;

    public void GetApple()
    {
        this.point += 100;
    }

    public void GetBomb()
    {
        this.point /= 2;
    }
    void Start()
    {
        this.generator = GameObject.Find("ItemGenerator");
        this.timerText = GameObject.Find("Time");
        this.pointText = GameObject.Find("Point");
    }

    public struct LevelData
    {
        public float start_time;
        public float end_time;
        public float generate_speed;
        public float fall_speed;
        public int bomb_ratio;
        public LevelData(float _start_time, float _end_time, float _generate_speed,
            float _fall_speed, int _bomb_ratio)
        {
            start_time = _start_time;
            end_time = _end_time;
            generate_speed = _generate_speed;
            fall_speed = _fall_speed;
            bomb_ratio = _bomb_ratio;
        }
    }
    public LevelData[] levelDataArr = new LevelData[4]
    {
        new LevelData(20.0f, 30.0f, 1.0f, -0.03f, 2),
        new LevelData(10.0f, 20.0f, 0.7f, -0.04f, 4),
        new LevelData(5.0f, 10.0f, 0.4f, -0.06f, 6),
        new LevelData(0.0f, 5.0f, 0.9f, -0.04f, 3),
    };

    void Update()
    {
        this.time -= Time.deltaTime;
        if(time < 0)
        {
            time = 0;
            this.generator.GetComponent<ItemGenerator>().enabled = false;
        }

        for(int i= 0; i <levelDataArr.Length; i++)
        {
            if (levelDataArr[i].start_time <= time && time < levelDataArr[i].end_time)
            {
                this.generator.GetComponent<ItemGenerator>().SetParameter(
                    levelDataArr[i].generate_speed,
                    levelDataArr[i].fall_speed,
                    levelDataArr[i].bomb_ratio
                    );
                break;
            }
        }

        /*if (this.time < 0)
        {
            this.time = 0;
            this.generator.GetComponent<ItemGenerator>().SetParameter(10000.0f, 0, 0);
        }
        else if (0 <= this.time && this.time < 5)
        {
            this.generator.GetComponent<ItemGenerator>().SetParameter(0.7f, -0.04f, 3);
        }
        else if (5 <= this.time && this.time < 12)
        {
            this.generator.GetComponent<ItemGenerator>().SetParameter(0.8f, -0.05f, 6);
        }
        else if (12 <= this.time && this.time < 20)
        {
            this.generator.GetComponent<ItemGenerator>().SetParameter(0.8f, -0.04f, 4);
        }
        else if (20 <= this.time && this.time < 30)
        {
            this.generator.GetComponent<ItemGenerator>().SetParameter(1.0f, -0.03f, 2);
        }*/
        this.timerText.GetComponent<Text>().text = this.time.ToString("F1");
        this.pointText.GetComponent<Text>().text = this.point.ToString() + " point";
    }
}
