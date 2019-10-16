using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    const float TopWallPosition = 3.7f;
    const float BottomWallPosition = -3.7f;
    const float TopSpikesPosition = 2.786f;
    const float BottomSpikesPosition = -2.786f;

    [SerializeField] private SpawnElement[] _walls = new SpawnElement[6];
    [SerializeField] private SpawnElement[] _spikes = new SpawnElement[6];

    private float _prevTopWallLenght;
    private Transform _prevTopWallTransfotm;
    private float _prevBottomWallLenght;
    private Transform _prevBottomWallTransfotm;

    private void Start()
    {
        SpawnOnTop();
        SpawnOnBottom();
    }

    private void Update()
    {
        if(transform.position.x - _prevTopWallTransfotm.position.x > _prevTopWallLenght)
        {
            SpawnOnTop();
        }

        if (transform.position.x - _prevBottomWallTransfotm.position.x > _prevBottomWallLenght)
        {
            SpawnOnBottom();
        }
    }

    public void SetSpeedToEnitys(float speed)
    {
        foreach(var wall in _walls)
        {
            wall.MovingEntityScript.SetSpeed(speed);
        }

        foreach (var spike in _spikes)
        {
            spike.MovingEntityScript.SetSpeed(speed);
        }
    }

    private float GetNewWallLenght()
    {
        return Random.Range(16f, 30f);
    }

    private void SpawnOnTop()
    {
        foreach (var item in _walls)
        {
            if(!item.GameObject.activeSelf)
            {
                item.GameObject.SetActive(true);
                item.MovingEntityScript.SetSpeed(GameManager.Instance.Speed);

                Transform tr = item.GameObject.transform;
                _prevTopWallTransfotm = tr;
                tr.position = new Vector3(transform.position.x, TopWallPosition, transform.position.z);

                float lenght = GetNewWallLenght();
                tr.localScale = new Vector3(lenght, tr.localScale.y, tr.localScale.z);
                _prevTopWallLenght = lenght + Random.Range(2f, 5f);

                foreach (var spike in _spikes)
                {
                    if(!spike.GameObject.activeSelf)
                    {
                        spike.GameObject.SetActive(true);
                        spike.MovingEntityScript.SetSpeed(GameManager.Instance.Speed);

                        Transform spikeTr = spike.GameObject.transform;
                        float spikePositionX = transform.position.x + Random.Range(2f, lenght - 2f);
                        spikeTr.position = new Vector3(spikePositionX, TopSpikesPosition, transform.position.z);

                        if (spikeTr.rotation == Quaternion.identity)
                        {
                            spikeTr.Rotate(new Vector3(0, 0, 180f));
                        }

                        return;

                    }
                }

                return;
            }
        }
    }

    private void SpawnOnBottom()
    {
        foreach (var item in _walls)
        {
            if (!item.GameObject.activeSelf)
            {
                item.GameObject.SetActive(true);
                item.MovingEntityScript.SetSpeed(GameManager.Instance.Speed);

                Transform tr = item.GameObject.transform;
                _prevBottomWallTransfotm = tr;
                tr.position = new Vector3(transform.position.x, BottomWallPosition, transform.position.z);

                float lenght = GetNewWallLenght();
                tr.localScale = new Vector3(lenght, tr.localScale.y, tr.localScale.z);
                _prevBottomWallLenght = lenght + Random.Range(2f, 5f);

                foreach (var spike in _spikes)
                {
                    if (!spike.GameObject.activeSelf)
                    {
                        spike.GameObject.SetActive(true);
                        spike.MovingEntityScript.SetSpeed(GameManager.Instance.Speed);

                        Transform spikeTr = spike.GameObject.transform;
                        float spikePositionX = transform.position.x + Random.Range(4f, lenght - 4f);
                        spikeTr.position = new Vector3(spikePositionX, BottomSpikesPosition, transform.position.z);

                        if(spikeTr.rotation != Quaternion.identity)
                        {
                            spikeTr.Rotate(new Vector3(0, 0, 180f));
                        }
                        return;
                    }
                }

                return;
            }
        }
    }

    
}
