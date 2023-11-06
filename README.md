# CMP304 Artificial Intelligence

![Header Image](ML_Agent.png)

If not interested in the Unity project, but only in the Scripts used, then the relevant scripts are located in Assets/Scripts - BeaconAgent.cs; Goal.cs; BeaconButton.cs; Wall.cs The logic for the ML Agent are in BeaconAgent, everything else is helper scripts used for the testing environment.

Project was built using unity version 2020.3.0f1, earlier versions are guaranteed to work.
ML Agents preview package used is 1.8.0-preview, earlier versions are not guaranteed to work
Python version used was 3.9.2, earlier versions are not guaranteed to work

When opening the project, you need to switch to the ML scene, in the hierarchy the Complex parent of gameobjects is the environment used for the testing of the module.

## Module Introduction
For this module we had to compare different AI techniques. Which those techniques are was up to us. During the semester we studied Finite State Machines, Rule Based Systems, Fuzzy Logic, Reinforcement and Predictive Learning, Genetic Algorithms, Case Based Reasoning, Artificial Neural Networks, Self-Organaing Maps and Clustering and Deep Neural Networks. I decided to compare 3 different techniques from Machine Learning - Reinforcement Learning and 2 types of Imitation Learning - Behavioral Cloning and GAIL (Generative Adversarial Imitation Learning)

### AI in games
Video Games have been using different Artificial Intelligence techniques since the very beginning. Although some of those techniques might not necessarily be classified as “intelligent” in 2021. Artificial Intelligence in games could be as simple as calculating where the paddle in  “Pong” should be, deciding the state and the behaviour of the ghosts in “Pac-Man” or beating the very best players in one of the most complex style of games we have to date, using Artificial Neural Networks (OpenAI) - “Dota 2”.

The goals of Games AI are often very different compared to the goals of Academic AI. In games AI serves the purpose to enhance the experience of the player as well as providing better entertainment or immersion. It does not focus on being better than the player and finding a balance between how strong the AI should be is often difficult. Another limiting factor in Games AI is computational power, often the computers of the players are far from the best in class, thus the Game AI is made to use much less resources than Academic AI.

An example of common Games AI techniques are Finite State Machines (FSM). One of the earliest uses of an FSM is the game “Pac-Man”. Each Ghost in the game has the same “Evade” state, which is triggered when the player interacts with a pill. Each Ghost also has its own implementation of the “Chase” state, giving each ghost a unique character. Transitioning from Evade to Chase happens on a timer, which when expires, the state changes. FSM’s are still commonly used in modern games for Non Playable Characters (NPC’s), a simple example is a guard who has a couple of states - Patrolling, Alert, Engaging.

### Project Introduction
This application demonstrates a different AI technique, Machine Learning. More precisely how three different types of machine learning compare against each other in a semi-complex example built in Unity. The runnable application includes the trained brains showcasing the respective results of the testing. The ML library used was the ML-Agents package from Unity. The three ML algorithms used were Reinforcement Learning, GAIL (Generative Adversarial Imitation Learning) and Behavioral Cloning. The test scenario in the application is a small environment which contains a player (who has an ML agent), a button/trigger, and a goal which is created when the button is pressed. A full test loop includes the player being created at a random point in the environment, the button being created in another random point, the player agent navigating successfully to the location of the button, interacting with it in order to spawn the goal, then locating the goal and the player agent reaching the goal. The inputs for the ML agent are using the unity input system. The inputs are the player movement on the Horizontal and Vertical axis and an interaction key (“E”) to use the button and spawn the goal.

This AI technique was chosen due to the vast potential it has for future implementation in Video Games. It was chosen to gain a deeper understanding of how ML works and how it could be used to train AI. The Test environment was selected in order to showcase strengths and weaknesses of the different algorithms.

### ML background
Machine Learning is a type of Artificial Intelligence that uses data to train and make more accurate predictions. There are three main ML algorithms - Unsupervised learning, Supervised Learning and Reinforcement Learning. Unsupervised Learning is aimed at helping identify patterns in data. It also requires to be fed the data wanted to be analysed beforehand and unlike Supervised, that data does not contain correct examples inside. It must analyze and categorize the data on its own. Supervised Learning, as the name suggests, has a supervisor as the teacher. That means that some of the data that is fed into the algorithm already contains the correct answer, it then uses that training data as an example and produces a correct outcome from the training data. Reinforcement Learning on the other hand, does not require input data beforehand. It relies on gathering the information from the environment by using different sensors, making observations and doing actions based on them. The final piece of reinforcement learning is the reward signal, the agent is trained to maximize its overall rewards, it is up to the user of the ML to define those rewards, negative and/or positive.

The test application utilized the Single-Agent and Simultaneous Single-Agent training models. A Single-Agent model is the traditional method of training an agent, a single environment and a single agent. The Simultaneous Single-Agent is a logical step up, again its one agent per environment, but there are multiple instances of them. Essentially parallelization of the training, which can speed up and stabilize the training process. After all, training 100 identical agents simultaneously is faster than training a single instance.

The underlying Reinforcement algorithm used by Unity’s Machine Learning toolkit is called Proximal Policy Optimization (PPO). This is a method that has been shown to be more general purpose and stable than many other Reinforced Learning algorithms. It is lighter than competing algorithms such as TRPO and ACER. It accomplishes that by a formula which better balances the policy grading by trying to compute an update at each step that minimizes the cost function while ensuring the deviation from the previous policy is relatively small.

There are two different Imitation Learning (IL) algorithms showcased by the application. Imitation learning could be more intuitive, because demonstrating to the Agent how it should perform and action should yield faster initial results compared to having it to learn from scratch by trial-and-error like Reinforced Learning. The first IL algorithm in the application is the Generative Adversarial Imitation Learning (GAIL) algorithm. The way the algorithm works is by having a second neural network, called the discriminator, be taught to distinguish whether a policy came from the demonstration given by a real player, or if the policy was produced by the agent. Essentially the goal of the agent is to trick the discriminator into thinking that the results were produced from the demo, not from the agent. At each training step the agent tries to maximize the reward, while the discriminator is trained to better distinguish between the demo and the agent, that in turn results in the agent getting better at copying the demo. The other IL algorithm is Behavioral Cloning (BC). As the name suggests this algorithm’s goal is to train the agent to copy the moves from the demo as close as possible.

The limitations of Imitation Learning is that they depend highly on the provided demo to do well in teaching the agent and they can not surpass said demo. That provides the agent a fast head start in learning how to do the given task. The limitation of Reinforced Learning is the opposite, it is slow to learn how to initially complete the task due to its trial-and-error approach, it may never even achieve its goal and learn “bad habits”, but unlike IL, RL does not have a limit on how good it can become at achieving the task, it can keep getting better. Thus the most optimal approach would be to combine the different algorithms and change the weight each has on the agent’s training at different stages. Imitation Learning and Reinforcement Learning supplement each other nicely.


### Methodology
The application has been developed using Unity and the Unity ML Agents toolkit due to familiarity with the engine, the fast iteration time of using the engine and the ease of use and setup of the ML Agents Toolkit. The agent is set up using the Behavior Parameters script provided by the toolkit and a BeaconAgent script used to define the main logic loop for the agent, including how to move, information it needs to find the targets, and the rewards. On the Behavior Parameters component of the agent the only needed setup is Behavior Name, it is used by the configuration script to locate the agent. The first thing that is done in the Agent script is the OnEpisodeBegin() function. An episode is the singular training session defined by the toolkit, a single failed or successful loop. In the case of the application at the start of an episode the agent and button positions need to be reset to a new, random one, the goal needs to be reset and removed, and the button visuals need to be reset. It is important to note that all positions for objects use localPosition, that is the case in order to support Simultaneous Single-Agent training models. That allows the environments and agents to be reset in their local bounds. Next in the Behavior Parameters component is the Vector Observation field, which is used to tell the agent the information it needs in order to orient itself in the world. In the case of the application that data contains the state of the button, if it can be used or not, the direction to the button, the state of the goal, if it has been spawned or not, and if the goal has been spawned, then the agent also receives the direction towards the goal, if it has not, the agent just receives zeros. In total there are 6 observations necessary for the agent, hence the Space Size field in the component is set to 6. Next is the OnActionReceived() function. It is responsible for giving meaning to the agent inputs with the game world. The agent actions are numerical values which then need to be associated with the values and inputs needed for the agent to accomplish the task. The agent uses Discrete Actions, the values are whole numbers, compared to Continuous Actions which are floats and are constrained from -1 to 1. Essentially the agent uses the discrete actions as booleans in the case of the application. The first action is reserved for the left/right movement state of the agent, a value of 1 would mean that it should move to the left, a value of 2 would mean that it should move to the right and a value of 0 means dont move. The second action is reserved for forwards/backwards movement of the agent similar to the first action. Based on the value of the actions, the agent’s velocity is modified. The third and final action is used for the “Interact” button. The agent is given a reward if it successfully uses the button. The agent also receives a negative reward based on the amount of actions it has performed, thus encouraging the agent to complete the tasks with the least amount of actions. The next function is Heuristic, which is a state in which a player takes control of the agent and the AI is not in the learning process. Heuristics are mainly used for testing of the environment before allowing the agent to start training. The final function, OnTriggerEnter() is used for collision detection, when the agent successfully reaches the goal, it is given a reward and the current training episode ends, if the agent touches a wall, it is given a negative reward and also ends the training episode.

The last setup needed in order to start training the agent is a configuration file. It uses the yaml file format. The most important parts for this application are the reward_signals and the behavioral_cloning parameters. There are two types of reward signals. The first is extrinsic, that is responsible for rewards to work and essentially enable Reinforced Learning, the two required parameters are strength and gamma. Strength is the factor by which to multiply the reward given by the environment and ranges from 0 to 1. Gamma is responsible for deciding how far into the future the agent should care about possible rewards. In situations when the agent should be acting in the present in order to prepare for rewards in the distant future, this value should be large. In cases when rewards are more immediate, it can be smaller. Gamme needs to be smaller than 1, so for the application it is always set to 0.99 due to the nature of the test environment. The other parameter in reward_signals is “gail”, adding it to the config enables the gail algorithm to be used, it requires a strength parameter as well as a demo_path. The behavioral_cloning parameter is put outside of the reward_signals. Like gail it also requires a strength and demo_path parameters. With all of that setup, the application can be ran and the agent will start learning based on the configuration file. The demo used to train GAIL and Behavioral cloning agents was recorded based on 100 episodes with an average cumulative reward of 1.9, the highest score possible is 2.

### Results
The Unity ML Agents Toolkit provides results which are nicely integrated into tensorboard. Two types of testing has been used. A single-agent scenario and simultaneous single-agent training. For the Single-agent training (figure 1), each agent was trained a hundred thousand episodes and the graphs are rated on the cumulative reward at every ten thousand sessions and the episode length, again at every ten thousand sessions. The Simultaneous single-agent training was done using 20 simultaneous agents for one million episodes (figure 2); they were rated on the same values as the single agent. There was also a test conducted that combined all 3 algorithms using a single-agent and trained for one hundred thousand episodes (figure 3).

#### Figure 1
![figure one graph](figure_one_graph.png)
![figure one legend](figure_one_legend.png)
###### Note: Behavioural refers to behavioral cloning; Imitation refers to GAIL; Reinforced refers to Reinforced Learning

#### Figure 2
![figure two graph](figure_two_graph.png)
![figure two legend](figure_two_legend.png)
###### Note: Behavioural refers to behavioral cloning; Imitation refers to GAIL; Reinforced refers to Reinforced Learning

#### Figure 3
![figure three graph](figure_three_graph.png)
![figure threelegend](figure_three_legend.png)
###### Note: Behavioural refers to behavioral cloning; Combined refers to all 3 algorithms together; Imitation refers to GAIL; Reinforced refers to Reinforced Learning

### Discussion of Results
The results proved interesting. The single agent results showcase the theoretically expected results. Reinforced learning coming in last, with the worst cumulative score and highest amount of steps needed to complete the episode. That was expected due to the nature of reinforced learning being slow to pickup on more complex tasks due to its trial-and-error approach. What was observed from the brain at the end was that this agent never got to the goal, it only learned to reach and interact with the button, which was an expected side effect, it picking up a bad and undesirable habit. That could be fixed by itself with more data and training.

A little surprising was that GAIL comes next in terms of cumulative score, not behavioral cloning. Due to the way the algorithm works it should be able to reach a better cumulative reward than behavioral cloning, due to the fact that it tries to improve a little past the demo, while behavioral can only get as good as the demo. That was most probably the case due to the small quantity of training, with more episodes, in theory, GAIL should provide better results over Behavioral Cloning. Another reason as to why BC has provided better early results could be to the fact that it does not have the overhead GAIL has, as in, it is trying to copy the demo exactly, while GAIL is trying to outsmart the discriminator, which takes more time to learn to do effectively.

Unsurprisingly combining all of the algorithms provided the best result in terms of cumulative reward. It is noteworthy that for the configuration of the combined learning the strength for the reinforced learning was set to 1, while both GAIL and BC were set to 0.5, this the agent prioritized reinforcement learning while benefiting from the headstart imitation learning techniques provided.

Another interesting result was that GAIL was able to complete the episode with the least amount of steps, compared to the other algorithms. Important to note is that episode length can greatly be affected by failed attempts as well, which count as low-step episodes.

The results from the simultaneous single agent tests are interesting. They completely contradict the results from the smaller testing sample, they also contradict the expected theoretical results. Reinforcement Learning came out on top by an undeniable margin, performing almost perfectly, while both GAIL and BC struggled to maintain a positive cumulative reward. These results were confirmed by re-doing them 3 times, each result being the same. The theory why GAIL and BC struggled as much is due to performance limitations and the train the ML took on the hardware. Although for that to be true, RL should have had similar results to both GAIL and BC. When viewing the results in the application using the generated brain, the reinforcement learning agent undoubtedly flies through the task and episode, while both BC and GAIL manage to pass it much like how an ogre would stumble through, they pass it, but not very gracefully not fast. Given enough training, it is expected that RL will provide better results than both BC and GAIL, but the graph shows that those differences happened extremely early in the testing and the gap barely closed. Considering the demo used for GAIL and BC had a cumulative rewards score of 1.9, in theory BC should have been able to get much closer to that score.

### Conclusion
In single agent testing episodes, all of the algorithms performed as expected. The only unexpected behavior was observed in the simultaneous single agent testing. In order to explore the reasons for the results better hardware should be used. The application test were conducted on a laptop with an i7-8750H CPU and GTX1060 max-q GPU. During testing, the simultaneous single agent application was running at ~4fps, during the testing of the single-agent, the application was running at ~15fps. The truth is that Machine Learning is computationally expensive. The application itself uses small assets and is very lightweight, when ran without the ML agent training on top, the application ran at ~120fps. Even for single agent training ML took its toll and pushed the hardware to its limits. Another proof of how far the application was pushed was that for most of the duration of the tests, the CPU/GPU temperatures recorded were in the range of 92-95 degrees Celsius.

Overall I am happy with the results and knowledge gained, it is a good basis for future exploration in the field and training more complex AI. Main takeaway is that the training should probably be done in smaller quantities but longer duration, given the hardware. The training can also be conducted in shorter, but multiple training sessions instead of a single hours long session.
