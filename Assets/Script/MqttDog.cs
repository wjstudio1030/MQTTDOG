using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using UnityEngine.SceneManagement;
using System.Threading;
using System;

public class MqttDog : MonoBehaviour
{
    public MqttClient client;
    public MqttMsgPublishEventArgs b;
    public string message;
    public bool NeedToRestart = false;
    [SerializeField] Projetile projetile;
    [SerializeField] ChangeSprite changesprite;
    [SerializeField] BigGun biggun;
    //[SerializeField] GameObject changup; 

    void Start()
    {
        // Create an MQTT client instance and specify the broker IP address.
        client = new MqttClient("mqttgo.io");

        // Register the method "Receive" to be executed when a message is published.
        client.MqttMsgPublishReceived += Receive;

        // Connect to the MQTT broker with a client ID "PLAYERCONTROLMQTT".
        client.Connect("PLAYERCONTROLMQTT");

        // Subscribe to two topics with QoS level 0 (at most once).
        client.Subscribe(new string[] { "jarvis/action/", "jarvis/actien/" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
    }

    void Update()
    {
        if (b != null)
        {
            string message = Encoding.UTF8.GetString(b.Message);
            if (message == "P")
            {
                changesprite.ChangeToSpriteDog();
                changesprite.ShowEffect.Play();
                changesprite.NextCat = true;
                changesprite.sprite.color = new Color(255f, 255f, 255f, 255f);
                projetile.CanShoot = true;
                projetile.Candogvoice = true;
                projetile.CanCatvoice = false;
                projetile.CanBirdvoice = false;
            }

            if (message == "O")
            {
                changesprite.ChangeToSpriteCat();
                changesprite.ShowEffect.Play();
                changesprite.NextBird = true;
                changesprite.sprite.color = new Color(255f, 255f, 255f, 255f);
                projetile.CanShoot = true;
                projetile.Candogvoice = false;
                projetile.CanCatvoice = true;
                projetile.CanBirdvoice = false;
            }

            if (message == "I")
            {
                changesprite.ChangeToSpriteBird();
                changesprite.ShowEffect.Play();
                changesprite.sprite.color = new Color(255f, 255f, 255f, 255f);
                projetile.CanShoot = true;
                projetile.Candogvoice = false;
                projetile.CanCatvoice = false;
                projetile.CanBirdvoice = true;
            }

            if (message == "T" && !projetile.hasProjectileLaunched && projetile.CanShoot)
            {
                projetile.hasProjectileLaunched = true;
                projetile.IsInvisible = true;
                projetile.CanShoot = false;
                projetile.CanHit = true;
                NeedToRestart = true;
                projetile.change.SetActive(false);
                // Launch the projectile
                projetile.angle = projetile._Angle * Mathf.Deg2Rad;

                projetile.StopAllCoroutines();

                projetile.StartCoroutine(projetile.Coroutine_Movement(projetile.ShowVelocity, projetile.angle));

                // Start the cooldown before the space key can be pressed again
                projetile.StartCoroutine(projetile.CooldownSpaceKey());

                SpriteRenderer sprite = GetComponent<SpriteRenderer>();

                projetile.StartCoroutine(projetile.DelayedAppear(2.5f));

                biggun.audiosource.Pause();

                //projetile.PlayAudioClip(audioclip1);
            }
            if (message == "R")
            {
                projetile.timeSinceLastChange += Time.deltaTime * 1000;
                if (projetile.timeSinceLastChange >= projetile.changeInterval)
                {
                    if (projetile.isIncreasing)
                    {
                        projetile.ShowVelocity += 1f;
                        if (projetile.ShowVelocity >= 14f)
                        {
                            projetile.ShowVelocity = 14f;
                            projetile.isIncreasing = false;
                        }
                    }
                    else
                    {
                        projetile.ShowVelocity -= 1f;
                        if (projetile.ShowVelocity <= 5f)
                        {
                            projetile.ShowVelocity = 5f;
                            projetile.isIncreasing = true;
                        }
                    }
                    projetile.timeSinceLastChange = 0f;
                }
                for (int i = 0; i < projetile.energyObjects.Length; i++)
                {
                    if (projetile.ShowVelocity >= i + 6)
                    {
                        projetile.energyObjects[i].SetActive(true);
                    }
                }
                for (int i = 0; i < projetile.energyObjects.Length; i++)
                {
                    if (projetile.ShowVelocity < i + 6)
                    {
                        projetile.energyObjects[i].SetActive(false);
                    }
                }
                biggun.audiosource.Play();
                projetile.change.SetActive(true);
                // UpdateChangeEnergy();
            }
            if (NeedToRestart)
            {
                projetile.ShowVelocity = 5f;
                NeedToRestart = false;
            }
            if (projetile.Candogvoice && message == "T")
            {
                StartCoroutine(projetile.PlayAudioClipsSequentially(projetile.audioclip1, projetile.audioclip4));
                projetile.raineffect.SetActive(true);
            }
            if (projetile.CanCatvoice && message == "T")
            {
                StartCoroutine(projetile.PlayAudioClipsSequentially(projetile.audioclip2, projetile.audioclip5));
                projetile.stoneeffect.SetActive(true);
            }
            if (projetile.CanBirdvoice && message == "T")
            {
                StartCoroutine(projetile.PlayAudioClipsSequentially(projetile.audioclip3, projetile.audioclip6));
                projetile.cometupeffect.SetActive(true);
                projetile.cometdowneffect.SetActive(true);
            }


            b = null; // Reset the variable to avoid executing the action continuously.
        }
    }
    /*void UpdateChangeEnergy()
    {
        for(int i = 5; i < changup.transform.childCount; i++)
        {
            if(projetile._InitialVelocity > i)
            {
                changup.transform.GetChild(i).gameObject.SetActive(true);
            }
            else{
                gameObject.SetActive(false);
                }
        }
    }
    */

    public void Receive(object sender, MqttMsgPublishEventArgs e)
    {
        Debug.Log("Received MQTT message. Topic: " + e.Topic + ", Message: " + Encoding.UTF8.GetString(e.Message));
        message = Encoding.UTF8.GetString(e.Message); // Update the message field
        b = e; // Store the message event args in 'b'.

    }

    private void OnDisable()
    {
        client.Unsubscribe(new string[] { "jarvis/action/", "jarvis/actien/" }); // Unsubscribe when the program is closing
        client.Disconnect();
    }
}
