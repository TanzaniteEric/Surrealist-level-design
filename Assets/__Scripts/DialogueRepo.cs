using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueRepo : MonoBehaviour
{
    public string[] playerDialogueOnHitDragonBlue;
    public string[] playerDialogueOnHitDragonRed;
    public string[] momDialogueOnAttackAngry;
    public string[] momDialogueOnAttackCalm;
    public string[] dadDialogueOnAttackAngry;
    public string[] dadDialogueOnAttackCalm;
    public string[] sisDialogueOnAttackAngry;
    public string[] sisDialogueOnAttackCalm;

    void Start()
    {
        playerDialogueOnHitDragonBlue = new string[]
        {
            "I will come back to visit whenever I can.", 
            "I’d rather pursue my dream and fail",
            "I CAN be responsible AND follow my dreams",
            "I can take care of myself, Don’t worry.",
            "I will never forget the town and my beloved family.", 
            "I know my responsibilities.",
            "Please let me make my own decisions.",
            "I’m not a child anymore.",
            "I will miss you so much but I have to go. "
        };

        playerDialogueOnHitDragonRed = new string[]
        {
            "Why would I waste my life in this boring town?",
            "You will never understand.",
            "The town is swallowing my dreams!",
            "Why don’t you let me make my decision!",
            "This is MY LIFE!",
            "I served the town for years! I have done my duty!",
            "No one cares what I want!",
            "Stop crying! You are so annoying!",
            "I’m not just a playmate for my sister!",
            "I have my dreams! Why can’t you understand?",
            "You are too old to know what passion is!",
            "Stay and waste my entire life here?",
            "Don’t treat me like that crying 4-year-old child!",
            "I know how to take care of myself!",
        };

        momDialogueOnAttackAngry = new string[]
        {
                "Such a spoiled child!",
                "I raised you! Show me some respect!",
                "Do you think you know more than I do? ",
                "You are too young to make these decisions.",
                "So impulsive! Now turn back! Go home!",
                "Reckless!",
                "You think you can just leave?",
                "You Are Staying!",
                "I've protected you all your life!",
                "You are blind to the dangers out there! ",
                "Horrible things might happen to you when you are away!",
        };

        momDialogueOnAttackCalm = new string[]
        {
                "The world is dangerous, I don’t want you to suffer.",
                "You’re safe with us, you don’t need to go anywhere.",
                "What if something happens to you when you are away? ",
                "You should stay under our protection.",
        };

        dadDialogueOnAttackAngry = new string[]
        {
                "The town is where we belong!", 
                "You CAN’T just leave us behind.",
                "Those who leave forget us and NEVER return!",
                "You’re TEARING this family apart!",
                "Dreams? How easily they crumble!",
                "You BETRAY us! We taught you! Nurture you! ",
                "Blinded by your selfish dreams!",
                "Don’t you dare leave!",
                "You're abandoning us!",
                "Abandoning your duty? I expect more from my child! ",
                "Irresponsible quitter! This is not my child!",
        };

        dadDialogueOnAttackCalm = new string[]
        {
                "Set aside your dreams and passion for the family and town, child, this is what responsible people do."
        };

        sisDialogueOnAttackAngry = new string[]
        {
                "No!!! Don’t leave me!",
                "I hate you and your stupid school!",
                "I will never see you again…",
                "You will forget us! You WILL!",
                "Don’t leave me alone!",
                "I will cry every day!",
                "I will be sooooo lonely! Stay for me!",
                "I need my sister! Don’t leave!",
                "But who will protect me when the night falls?",
                "My life will be colorless without you. ",
                "I will miss your hugs! Please don’t leave me.",
                "I can’t sleep without your accompany!",
                "Please please please please!",
                "My life will be miserable without my sister. ",
        };

        sisDialogueOnAttackCalm = new string[]
        {
                "I love you, sister, why can’t we stay together forever?",
        };
    }
}
