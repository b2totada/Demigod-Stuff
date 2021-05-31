using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;


public class PlatformTest
{
    private GameObject testObject;
    private PlatformScript platformScript;

    [SetUp]
    public void Setup()
    {
        testObject = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Demigod/Prefabs/MovingPlatform.prefab");
        testObject = GameObject.Instantiate(testObject);
        platformScript = testObject.AddComponent<PlatformScript>();
    }

    [UnityTest]
    public IEnumerator PlatformComesWithScript()
    {
        yield return new WaitForSeconds(0.1f);

        Assert.IsTrue(platformScript != null, "ExampleScript must be set on Example.prefab.");
    }

    [UnityTest]
    public IEnumerator PlatformComesWithRigidbody2D()
    {
        yield return new WaitForSeconds(0.1f);

        Assert.NotNull(testObject.GetComponent<Rigidbody2D>(), "Platform has Rigidbody2D");
    }

    [UnityTest]
    public IEnumerator PlatformMoves()
    {
        Vector3 position = testObject.transform.position;

        yield return new WaitForSeconds(0.1f);

        Vector3 newPosition = testObject.transform.position;

        Assert.AreNotEqual(newPosition, position, "Passed. Platform moved from " + position + " to " + newPosition);
    }

    [TearDown]
    public void TearDown()
    {
        GameObject.Destroy(testObject);
    }
}
