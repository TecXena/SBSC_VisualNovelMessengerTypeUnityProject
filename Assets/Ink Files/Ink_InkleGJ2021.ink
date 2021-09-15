/*
List of Tags========================
#Mode noPhone/Phone/NarrateWithPhone
    #Mode noPhone - Hides the Phone from the scene, narration is through a text box
    #Mode Phone - Shows the Phone, narration is through the phone
    #Mode NarrativeWithPhone - Shows the phone, narration is on text box
#AddPoint V/A
    #AddPoint V - Adds a point to V_points
    #AddPoint A - Adds a point to Abe_points
#BG day/night
    #BG day - Changes the bg to day 
    #BG night - Changes the bg to night
#Ending V/A/MC
    #Ending V - Deletes previous GC messages and shows text in V Knot
    #Ending A - Deletes previous GC messages and shows text in A Knot
    #Ending MC - Shows text of Neutral end
#ShowSelfie V/A
    #ShowSelfie V - Transitions to the end scene of V and MC
    #ShowSelfie A - Transitions to the end scene of A and MC
#User V/A/MC
    #User V - Creates a message for V
    #User A - Creates a message for A
    #User MC - Creates a message for MC
#PhoneTime [Value]
    - Sets the text value of phoneTime
    - No space in between
    - Ex. 5:10AM
#PhoneDay [Value]
    - Sets the text value of phoneDay
    - Mon[dayNo.]
    - No space in between
    - Ex. Aug27
#Transition Start/End
#ShowSubtitle start/phone

==================================
*/


VAR charChosen = ""
VAR charUnchosen = ""
LIST characters = V, Abe


// Experiment with the points variables to check if it's working
VAR V_points = 0
VAR Abe_points = 0
VAR MCChoseSomeone = 0

-> FirstScene


/* When setting the BG, Mode, PhoneTime and Phone Day tags
    set it up in the first line of the knot
    
    Ex.
    === Knot1 ===
    #BG day
    #ModePhone
    #PhoneDay Aug27
    #PhoneTime 8:01AM
*/
=== FirstScene ===
/*
How to handle the transition and the meta data of knots:

1. last line #Transition start [Skip this on the first part]
2. Sample Hidden Text #Meta data tags (BG, Phone Time, Phone Day, Convo) and then #Skip
3. sample Hidden Text 2 #Transition End #Skip
4. Current Text #Phone Mode
*/

//->SecondScene
//Debug For Modes
// Would be nice if the Modes of NoPhone And NarrativWithPhone was connected to the dialouge Box -> DONE, but still requires to paste the mode everytime
/*
NoPhone at first #Mode noPhone
Then show phone #Mode Phone
Then narrative with phone #Mode NarrativeWithPhone
*/

#BG day
#PhoneTime 3:15PM
#PhoneDay Jun21
#ShowSubtitle start
sample hidden text 


#Mode noPhone
<b>"Uhnn... What time is it? 3 p.m..."
I stayed in my bed, lazy to get up. 
It's really nice to have sis back. Or else I won't hear the end of it from mom... 
I picked up my phone and checked our chatroom.#Mode NarrativeWithPhone

I stare at my phone for a few minutes. What should I say?..  
//-> Final_Scene //Debug test to end

[Hidden Text]#Mode Phone #ShowSubtitle phone 
 * Hello! ^^ #User MC 
    <i>........huh#Mode NarrativeWithPhone
    It's rare to see those two offline... 
    <b>"Oh well." 
    
#Mode noPhone
Ever since the summer break began, I've been feeling anxious. 
Just one more year and, we're off to universities. 
I don't even know what I want to do, what to choose. 
They say it'll all work out but... 
<i>Sigh...
<b>"I'll go sleep again..." 


-> SecondScene

=== SecondScene ===
// do transition tag first then have a text for the other remaining tags, skipping the dialouge automatically 
#Skip knots
...//hidden text
#BG night
#PhoneDay Jun21
#PhoneTime 8:27PM
...//hidden text



#Mode noPhone
When I woke up, it's already around 8:30. I checked my phone again.

/*
For each user messsahe, have a tag in the end 
    to make sure the message will be shown as them
*/
// Create previous Messages as a function
#Mode Phone
how do you manage to follow your stupidly strick schedule#User V
It's called being efficient.#User A
your a robot, there's no other explination #User V
You're#User A
oh god,, if not, i'm pretty sure your a lizard folk or something#User V 
i need help#User V
@MC#User V
@MC!! i summon you! #User V

    *[Yes, hello~]Yes, hello V~#User MC #Addpoint V
    another normal human being, finally#User V
    ->SecondScene.SecondSub
    *[You're*]You're*#User MC #Addpoint A
    ugghh#User V
    ->SecondScene.SecondSub



    = SecondSub
    
    Goodevening Margarette #User A
    ^^"#User MC
    Evening. I slept through the day again, haha. #User MC
    Excesive sleep can be bad for you. #User A
    mood #User V
    Are you eating properly?#User A
    I try to, don't worry. #User MC
    as long as u put food in your mouth, you'll be fine #User V
    No, it's not enough. No matter how trivial it is you should always pay attention to your diet. #User A
    calm dooowwwnn#User V
    I specifically remember you mixing energy drinks, chocolate and soda together. Saying it's for "Science"#User A
    it is! i was planning to try it wit coffee this time#User V
    we'll see how long i can stay awake #User V
    I'm surprised you're not in the hospital. #User A
    ;DD#User V
    You're a danger to yourself.#User A
    Please don't be like him, Margarette.#User A

    *[I won't.] Don't worryy, I won't. V is on another level and I don't think I can copy that. #User MC #Addpoint A
        Good. That's a relief. :) #User A
        ->SecondScene.SecondSub2
    *I'm kind of curious[], tbh. How did you make that??#User MC #Addpoint V
        lol, later#User V
        Are you really going to try to make yourself sick? #User A
        Idiots. #User A
        ->SecondScene.SecondSub2
        
    = SecondSub2
    do you guys want to watch ghibli films tonight? #User V
    You're going to watch the cats again aren't you? #User A
    it's gooood #User V
    I prefer the Wind Rises. Or better yet, the Grave of the Fireflies.#User A
    those were the least entertaining#User V
    It's based on history. #User A
    lol, #User V
    [V started the call] #User V

    #Mode noPhone
    We ended up watching Spirited Away by the end. It was a fun night. 
    Maybe I should try doing something else tomorrow..? 
    I feel like I'm depending whether or not those two are online. 
    
    ->ThirdScene 

=== ThirdScene ===
#Skip knots
...//hidden text
#BG day
#PhoneTime 10:12AM
#PhoneDay Jun30
...//hidden text


#Mode Phone
My father once again expresses his dissapointment. #User A 
Saying, that I won't have a clear future by starting my own business. #User A
But what does he know? #User A

*Oof#User MC #Addpoint V
    ?? #User A
*That must've been hard..[]I'm sorry. #User MC #Addpoint A
    Don't be. This is quite normal for him #User A
- ah, classic dad #User V
    good thing i don't have one #User V

*You don't have one?#User MC #Addpoint V
    i mean he's dead to me #User V
    Something happened? #User MC
    in another time, i'll tell you.#User V
*lol  #Addpoint A
    like, seriously. #User V
- Unlike Vivi, I do not hate him. Hate is a strong word, I'm rather annoyed by it. #User A
    we know, we know #User V
    where's rocky?#User V
    Asleep.#User A
    man I miss your dog #User V


*Rocky is really cute. #User MC #Addpoint A
    yeah! he's such a good boy#User V
    If we ever meet Margarette, I'll bring Rocky with me. #User A
    MC:Yes! #User MC
*The dog kinda scares me.#User MC #Addpoint V
    Why is that? #User A
    You told me once he broke someone leg right? #User MC
    That's because my life was danger. Rocky acted ot of duty. #User A
    yeah, he's safer than you think #User V
- i only met rocky once but if anything happends to him i would kill everyone and then myself #User V
    Agree. His my only loyal company.  #User A
    only? what about me? D:#User V
    What do you mean?#User A
    harsh#User V


*It's hard to take V seriously.#User MC #Addpoint A
    lol okay#User V
    but, in all honesty, i'm always sincere with you guys #User V
*Don't forget about us~#User MC #Addpoint V
    we've been with you for like, 2 years#User V
    well, 3 if you count us in 4th grade#User V
- I meant, companion as in pet. I do consider you guys as my friends, or else I would rather spend my free time doing something else. #User A
    aawwwweee #User V
    Anyways, I should be going. There's some things I need to take care of. Take care.#User A
    yeah, you too. and I.. still need to do chores, see you guys later #User V
    Byee!#User MC

I logged off. Feeling a bit dissapointed as I still wanted to contine the conversation. #Mode noPhone
Now I'm alone with my thoughts and emotions... 
I should just sleep. 
    -> Final_Scene
    
=== Final_Scene ===
#Skip knots
...//hidden text
#BG night
#Mode NarrativeWithPhone
#PhoneTime 9:01PM
#PhoneDay Jul27
...//hidden text

{
- V_points > Abe_points:
    ~ charChosen = characters.V
    ~ charUnchosen = characters.Abe
    ~ MCChoseSomeone = 1
- Abe_points > V_points:
    ~ charChosen = characters.Abe
    ~ charUnchosen = characters.V
    ~ MCChoseSomeone = 1
- V_points == Abe_points:
    ~ MCChoseSomeone = 0 
}


{
- MCChoseSomeone == 1: I stare at my phone thinking of what I should do. 
    My routine didn't change.. It's been such a boring break. 
{charUnchosen} haven't been online that much, and I've been talking to {charChosen} more often. 
    I wish we can meet in real life. It sucks to find better friends online who are far from you.//Note to Pen, create premade messages to the private convo
- MCChoseSomeone == 0: I stare at my phone thinking of what I should do. 
    My routine didn't change.. It's been such a boring break. 
    Abe and V haven't been online that much and I've been all by myself. 
    All I can do is distract myself or sleep to avoid thinking about it. 
    #Ending MC //Move to ending scene 
    ...
    -> END
}

{
- V_points > Abe_points:
    ->VScene
- Abe_points > V_points:
    -> AbeScene
}
//If points are equal make an additional choice, it will let the MC chat Abe or V but they won't get a reply. Pen: Let's just skip to the neutral ending lol
//neutral ending would be the bedroom bg 
//I stare at my phone thinking of what I should do. My routine didn't change.. It's been such a boring break. Abe and V haven't been online that much that I've been by myself. All I can do is distract myself or sleep.. Summer break is ending soon.. [sigh]

=== AbeScene ===
#Mode Phone
#ShowPM A
Hey,, #User MC
Hello, how are you? #User A
Are you busy? #User MC
Not at the moment, why? Is something wrong? #User A
Oh, nothing really it's just that, I don't know I've been feeling anxious lately.#User MC 
Why is that?#User A
I kept thinking about, what I want to do in my life?#User MC
If you want, I can help you narrow down options. I may not be able to help you feel better but I can suggest solutions. #User A
Thank you, and no worries, I think I need that kind of approach right now. #User MC
You and Vivi have met right? #User MC
Yes, back in 4th grade. #User A
Oh, I mean see each other, physically. Last year?#User MC
Yes, we did. #User A
I was wondering maybe we could meet up someday. #User MC
If one of my relatives fly over to your place, I'll try to. #User A
Promise?#User MC
Promise.#User A
I'll look forward to it :D#User MC
#Ending A
  -> END
  
=== VScene ===
#Mode Phone
#ShowPM V
Hey,,#User MC
yooooo#User V
What are yo doing?#User MC
breathing, amongst other things, what's up?#User V
Oh nothing, it's jst that i've been feeling anxious again..#User MC
well, do you want to rant or wanna do something? watch a movie?#User V
No... chatting is fine.. I just don't want to be alone with my thoughts#User MC
i see, i see #User V
what's bothering you exactly?#User V
Everything? And somehow it escalates to me dying alone in a ditch#User MC
come on, you're too good for that #User V
you'll manage, you made it until now and you'll make it further#User V
Thanks, I really apreciate that :"D..#User MC
You and Abe have met right? When you stayed at your uncle's house? #User MC
oh yeah, it was so fun, you should've been there #User V
I was thinking maybe we could meet up someday. #User MC
yes! once i get my liscence #User V
Promise?#User MC
totally#User V
I'll look forward to it :D#User MC
#Ending V
  -> END






 

  