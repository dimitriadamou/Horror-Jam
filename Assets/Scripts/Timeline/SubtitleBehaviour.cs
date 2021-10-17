using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SubtitleBehaviour : PlayableBehaviour
{
    public string subtitleText; 

    private bool ran = false;

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        ran = false;
    }
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if(ran) return;
        ran = true;
        StringEvent stringEvent = playerData as StringEvent;

        stringEvent.FireEvent(
            subtitleText
        );
    }
}
