
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationHelper : MonoBehaviour
{
    public IEnumerator SlideIn(RectTransform transform, Vector2 EndPosition, Direction direction, float speed, UnityEvent? OnEnd)
    {
        Vector2 startposition;
        switch (direction)
        {
            case Direction.UP:
                startposition = new Vector2(0, -Screen.height);
                break;
            case Direction.DOWM:
                startposition = new Vector2(0, Screen.height);
                break;
            case Direction.LEFT:
                startposition = new Vector2(Screen.width, 0);
                break;
            case Direction.RIGHT:
                startposition = new Vector2(-Screen.width, 0);
                break;
            default:
                startposition = new Vector2(0, -Screen.height);
                break;
        }

        float time = 0;
        float during = 1f;
        while (time < during)
        {
            transform.anchoredPosition = Vector2.Lerp(startposition, EndPosition, time);
            yield return null;
            time += Time.deltaTime * speed;
        }
        transform.anchoredPosition = startposition;
    }
}
