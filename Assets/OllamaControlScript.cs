using ollama;
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class OllamaControlScript : MonoBehaviour
{
    private string demoModel = "phi4-mini:latest";
    public Text llmOutput;
    private int food = 30;
    private int water = 70;

    public Slider foodSlider;
    public Slider waterSlider;

    private string userInput;

    // Streaming
    private Queue<string> buffer = new Queue<string>();
    private bool isStreaming;
    
    void Start()
    {
        Ollama.InitChat();
    }
    public async void chat()
    {
        if (isStreaming)
            return;

        isStreaming = true;
        userInput = string.Format("You are a videogame character. Your hungerbar is {0}% full and your thirstbar is {1}% full. Limit your response to one of two words 'eat' or 'drink'.", food, water);
        Debug.Log(userInput);
        await Ollama.ChatStream((string text) => buffer.Enqueue(text), demoModel, userInput);

        isStreaming = false;
    }

    void Update()
    {
        foodSlider.value = food;
        waterSlider.value = water;

        if (Random.Range(0, 75) == 0)
        {
            if (Random.Range(0, 2) == 0)
            {
                food -= Random.Range(1, 3);
            }
            else
            {
                water -= Random.Range(1, 3);
            }
        }

        if (!isStreaming)
        {
            chat();
            return;
        }

        while (buffer.TryDequeue(out string text))
        {
            text = text.ToLower();
            Debug.Log(text);
            if (text.Contains("eat")) { 
                food = 100;
                transform.position = new Vector3(-8, -3.2f, 0);
            } 
            else { 
                water = 100;
                transform.position = new Vector3(7, -3.2f, 0);
            }
        }
    }
}

