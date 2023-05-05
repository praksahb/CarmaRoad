using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class AnimalController
{
    // PUBLIC LEVEL -
    // read-only properties
    public AnimalModel AnimalModel { get; }
    public AnimalView AnimalView { get; }
    //  might have to provide ASM with write access if changing states from animalView or here 
    public AnimalStateManager AnimalStateManager { get; }
    // Ctor - handles individual animal - used in CreateAnimalService
    public AnimalController(AnimalModel animalModel, AnimalView animalView, Vector2 spawnPoint, AnimalSpawnPosition animalSpawnPos)
    {
        AnimalModel = animalModel;

        Quaternion rotation = SetFacingDirection(animalSpawnPos);
        AnimalView = Object.Instantiate(animalView, spawnPoint, rotation);
        AnimalView.AnimalController = this;


        AnimalStateManager = AnimalView.GetComponent<AnimalStateManager>();
        AnimalStateManager.AnimalController = this;

        InitializeAnimationClipDictionary();
    }

    // main move function for 2d animal
    // is it better to move animal which is a kinematic rigidbody with the movePosition in every fixed update or by setting velocity value once in the start function
    public void MoveAnimal(bool isRunning = false)
    {
        AnimalModel.IsIdle = false;
        AnimalModel.IsRunning = isRunning;

        Vector2 velocity = new Vector2(AnimalModel.CurrentSpeed, 0) * AnimalView.transform.right;

        Vector2 deltaMove = AnimalView.RBody2D.position + velocity * Time.fixedDeltaTime;
        AnimalView.RBody2D.MovePosition(deltaMove);
    }
    // stops moving 2d animal
    public void StopMoving()
    {
        AnimalModel.IsIdle = true;
        AnimalView.RBody2D.velocity = Vector2.zero;
    }
    //change direction
    public void ChangeDirection()
    {
        Quaternion newRotation = AnimalView.transform.rotation * Quaternion.Euler(0, 180, 0);
        AnimalView.transform.rotation = Quaternion.Slerp(AnimalView.transform.rotation, newRotation, 0.8f);
    }

    // plays animation according to  state
    public void PlayAnimation(AnimalAnimationClip clip)
    {
        int hashVal;
        if (animationClips.TryGetValue(clip, out hashVal))
        {
            AnimalView.animatorController.Play(hashVal);
        }
        else
        {
            Debug.LogError($"Key name: {clip} doesnt exist");
        }
    }

    // Private Level -
    // dictionary for storing hash values of animationClips string names
    private Dictionary<AnimalAnimationClip, int> animationClips;

    // changes transform.right value of animal to point in left or right direction of the road
    private Quaternion SetFacingDirection(AnimalSpawnPosition animalSpawnPos)
    {
        if (animalSpawnPos == AnimalSpawnPosition.Right)
        {
            return Quaternion.Euler(0, 180, 0);
        }
        else
        {
            return Quaternion.Euler(0, 0, 0);
        }
    }
    // initialize hash values for animation clip names
    private void InitializeAnimationClipDictionary()
    {
        animationClips = new Dictionary<AnimalAnimationClip, int>();

        for (int i = (int)AnimalAnimationClip.Idle; i <= (int)AnimalAnimationClip.Dead; i++)
        {
            string clipName = GetAnimClipName((AnimalAnimationClip)i);
            int hashValue = Animator.StringToHash(clipName);
            animationClips.Add((AnimalAnimationClip)i, hashValue);
        }
    }
    // helper for above func
    private string GetAnimClipName(AnimalAnimationClip clip)
    {
        string clipName = "";
        switch (clip)
        {
            case AnimalAnimationClip.Idle:
                clipName = "Idle";
                break;
            case AnimalAnimationClip.Walk:
                clipName = "Walk";
                break;
            case AnimalAnimationClip.Run:
                clipName = "Run";
                break;
            case AnimalAnimationClip.Dead:
                clipName = "Dead";
                break;
            default:
                Debug.LogError("Invalid animation clip");
                break;
        }
        return clipName;
    }
}
