using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face
{
    public GameObject _gameObject = null;
    public Vector3 _centerOfFace = Vector3.zero;

    public List<Cube> _cubeInFace = null;

    public Face()
    {
        _cubeInFace = new List<Cube>();
    }

    public void Reset()
    {
        _gameObject.transform.localRotation = Quaternion.identity;
    }

    public void SetCubesToChildren()
    {
        for (int i = 0; i < _cubeInFace.Count; ++i)
        {
            _cubeInFace[i].transform.parent = _gameObject.transform;
        }
    }

    public void SetCubesToParent(Transform parent)
    {
        for (int i = 0; i < _cubeInFace.Count; ++i)
        {
            _cubeInFace[i].transform.parent = parent;
        }
    }

    // Rotate the face around the World(0,0,0)
    public void RotateAxisAngle(Vector3 axis, float angle)
    {
        // Get the quaternion of rotation
        Quaternion rotation = Quaternion.AngleAxis(angle, axis);

        // Rotate the parent gameObject to rotate the whole face
        _gameObject.transform.localRotation = rotation * _gameObject.transform.localRotation;
        
        //***** OLD *****// Rotation all the cubes around the center 
        //Transform currTransform;
        //// Rotate the cube
        //for (int i = 0; i < _cubeInFace.Count; i++)
        //{
        //    currTransform = _cubeInFace[i].transform;
        //    currTransform.localPosition = rotation * currTransform.localPosition;
        //    currTransform.localRotation = rotation * currTransform.localRotation;
        //}
    }

    public bool CheckFaceDone()
    {
        List<Quaternion> SpriteRot = new List<Quaternion>();
        List<Sprite> SpritePossible = new List<Sprite>();

        int index = 0;
        if (_cubeInFace.Count % 2 == 1)
            index = ((_cubeInFace.Count - 1) / 2) + 1;
        else
            index = ((_cubeInFace.Count - 1) / 2);

        Cube currentCube = _cubeInFace[index -1 ];
        for (int i = 0; i < currentCube._spriteList.Count; i++)
        {
            SpritePossible.Add(currentCube._spriteList[i].sprite);
            SpriteRot.Add(currentCube._spriteList[i].transform.rotation);
        }

        SpriteRenderer currentSpriteRenderer = null;
        for (int i = 0; i < _cubeInFace.Count; i++)
        {
            currentCube = _cubeInFace[i];

            int j = 0;
            if (!SpriteIsPossible(currentCube, SpritePossible, ref j))
                return false;

            currentSpriteRenderer = currentCube._spriteList[j];

            bool At90degre = false;
            switch (switchPosition)
            {
                case 0:
                    if (currentSpriteRenderer.transform.rotation.eulerAngles.y < SpriteRot[0].rotation.eulerAngles.y + epsilon &&
                        currentSpriteRenderer.transform.rotation.eulerAngles.y > SpriteRot[0].rotation.eulerAngles.y - epsilon &&
                        currentSpriteRenderer.transform.rotation.eulerAngles.x < SpriteRot[0].rotation.eulerAngles.x + epsilon &&
                        currentSpriteRenderer.transform.rotation.eulerAngles.x > SpriteRot[0].rotation.eulerAngles.x - epsilon)
                        At90degre = true;
                    break;

                case 1:
                    if (currentSpriteRenderer.transform.rotation.eulerAngles.x < SpriteRot[0].rotation.eulerAngles.x + epsilon &&
                        currentSpriteRenderer.transform.rotation.eulerAngles.x > SpriteRot[0].rotation.eulerAngles.x - epsilon &&
                        currentSpriteRenderer.transform.rotation.eulerAngles.y < SpriteRot[0].rotation.eulerAngles.y + epsilon &&
                        currentSpriteRenderer.transform.rotation.eulerAngles.y > SpriteRot[0].rotation.eulerAngles.y - epsilon)
                        At90degre = true;
                    break;

                case 2:
                    if (currentSpriteRenderer.transform.rotation.eulerAngles.x < SpriteRot[0].rotation.eulerAngles.x + epsilon &&
                        currentSpriteRenderer.transform.rotation.eulerAngles.x > SpriteRot[0].rotation.eulerAngles.x - epsilon &&
                        currentSpriteRenderer.transform.rotation.eulerAngles.y < SpriteRot[0].rotation.eulerAngles.y + epsilon &&
                        currentSpriteRenderer.transform.rotation.eulerAngles.y > SpriteRot[0].rotation.eulerAngles.y - epsilon)
                        At90degre = true;
                    break;

                default:
                    break;
            }

            if (!At90degre)
                return false;
        }
        return true;
    }
}
