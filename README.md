# Youtube Player

>> Current Version: 1.0.1

## About This Project
Youtube Player is a windows form control designed to play youtube videos right there in your windows form. It is very simple and easy to use.

## Using This Control
To use this control, you must first reference the youtube player dll in your project. Then, either add the control manually in your code, or add it to your VS toolbox, you can show the control in your code.

### Via Code
import YoutubePlayer;
> public void InitPlayer()
> {
>   Player videoPlayer = new VideoPlayer();
>	videoPlayer.VideoID = "Some_video_id";
>	this.Controls.Add(videoPlayer);
>	// Viola! Player is added
> }

## Why This Project?
I've recently noticed a need in one of my projects for a youtube player. To make my project more clean and organized, I decided to make this control to make things easier. I hope it comes handy to someone else who will need it.

## Screenshots

![VS Designer](https://i.imgur.com/RdfxwV3.png)
![The control in action on a live form](https://i.imgur.com/eaqRV8d.png)
![The project in refrences](https://i.imgur.com/c7uNDux.png)
![The control in the toolbox](https://i.imgur.com/Zx5412h.png)
![The properties for the control with the VideoID property highlighted](https://i.imgur.com/IgtIY29.png)
