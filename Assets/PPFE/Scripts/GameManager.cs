using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject Sounds;
    [SerializeField] private GameObject Polaroid;
    [SerializeField] private GameObject Animals;
    [SerializeField] private GameObject Accessories;
    [SerializeField] private GameObject RedButton;
    [SerializeField] private GameObject SphereRoom;
    [SerializeField] private GameObject RestOfRoom;

    public void StartGame()
    {
        Sounds.SetActive(true);
        Polaroid.SetActive(true);
        Animals.SetActive(true);
        Accessories.SetActive(true);

        RedButton.SetActive(false);
        RestOfRoom.SetActive(false);

        //StartCoroutine(ActivateProps());
        StartCoroutine(Transition(5f));
    }

    public IEnumerator ActivateProps()
    {
        if (!Animals.activeSelf)
        {
            if (!Polaroid.activeSelf)
            {
                if (!Accessories.activeSelf)
                {
                    if (!Sounds.activeSelf)
                    {
                        Sounds.SetActive(true);
                        
                        yield return new WaitForSeconds(0.5f);
                    }
                    Accessories.SetActive(true);
                    yield return new WaitForSeconds(0.5f);
                }
                Polaroid.SetActive(true);
                yield return new WaitForSeconds(0.5f);
            }
            Animals.SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
        yield return null;
    }

    public IEnumerator Transition(float seconds)
    {
        float elapsedTime = 0;
        while (elapsedTime < seconds)
        {
            SphereRoom.transform.localScale += new Vector3(1,1,1) * Time.deltaTime * 50f;
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        SphereRoom.SetActive(false);
    }

}
