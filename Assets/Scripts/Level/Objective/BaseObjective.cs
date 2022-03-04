

[System.Serializable]
public abstract class BaseObjective 
{
    protected bool complete;

    public BaseObjective()
    {
        ResetObjective();
    }

    public abstract void ResetObjective();
    
    public void Complete()
    {
        complete = true;
    }

    public bool GetStatus()
    {
        return complete;
    }

    public abstract string Describe();
    public abstract void Subscribe();
    public abstract void Unsubscribe();
    public abstract void UpdateStatus();
}
