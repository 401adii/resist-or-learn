using System;
using System.Collections.Generic;

namespace resist_or_learn;

public static class SceneManager
{
    private static Stack<IScene> sceneStack;

    public static void AddScene(IScene scene)
    {
        scene.Load();
        sceneStack.Push(scene);
    }

    public static void RemoveScene()
    {
        sceneStack.Pop();
    }

    public static IScene GetCurrentScene()
    {
        return sceneStack.Peek();
    }
    
}
