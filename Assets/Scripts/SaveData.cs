using UnityEngine;

[System.Serializable]
public abstract class SaveData
{
    private bool isEnabled;
    private float[] pos;

    public void SetEnabled(bool isEnabled)
    {
        this.isEnabled = isEnabled;
    }

    public bool GetEnabled()
    {
        return isEnabled;
    }

    public void SetPos(Vector3 position)
    {
        pos = new float[] { position.x, position.y, position.z };
    }

    public Vector3 GetPos()
    {
        return new Vector3(pos[0], pos[1], pos[2]);
    }
}
