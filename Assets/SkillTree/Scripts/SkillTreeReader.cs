using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SkillTreeReader : MonoBehaviour 
{
    private static SkillTreeReader _instance;
    public static SkillTreeReader Instance
    {
        get
        {
            return _instance;
        }
        set
        {
        }
    }

    private Skill[] _skillTree;
    private Dictionary<int, Skill> _skills;
    private Skill _skillInspected;
    public int availablePoints = 100;

    void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
            SetUpSkillTree();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

	void SetUpSkillTree ()
    {
        _skills = new Dictionary<int, Skill>();

        LoadSkillTree();
	}

    public void LoadSkillTree()
    {
        string path = "Assets/SkillTree/Data/skilltree.json";
        string dataAsJson;
        if (File.Exists(path))
        {
            dataAsJson = File.ReadAllText(path);

            SkillTree loadedData = JsonUtility.FromJson<SkillTree>(dataAsJson);

            _skillTree = new Skill[loadedData.skilltree.Length];
            _skillTree = loadedData.skilltree;

            for (int i = 0; i < _skillTree.Length; ++i)
            {
                _skills.Add(_skillTree[i].id, _skillTree[i]);
            }
        }
        else
        {
            Debug.LogError("Cannot load game data!");
        }        
    }

    public bool IsSkillUnlocked(int id)
    {
        bool skillExists = _skills.TryGetValue(id, out _skillInspected);

        if (skillExists)
        {
            return _skillInspected.unlocked;
        }
        else
        {
            return false;
        }
    }

    public bool CanSkillBeUnlocked(int id)
    {
        bool skillExists = _skills.TryGetValue(id, out _skillInspected);
        bool enoughPoints = _skillInspected.price <= availablePoints;


        bool canUnlock = true;
        if (skillExists)
        {
            if (enoughPoints)
            {
                int[] dependencies = _skillInspected.dependencies;
                for (int i = 0; i < dependencies.Length; ++i)
                {
                    if (DependencyExists(i))
                    {
                        if (!_skillInspected.unlocked)
                        {
                            canUnlock = false;
                            break;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
            
        }
        else 
        {
            return false;
        }
        return canUnlock;
    }

    private bool DependencyExists(int id) => _skills.TryGetValue(_skillInspected.dependencies[id], out _skillInspected);

    public bool UnlockSkill(int id)
    {
        bool skillExists = _skills.TryGetValue(id, out _skillInspected);
        bool enoughPoints = _skillInspected.price <= availablePoints;

        if(skillExists)
        {
            if (enoughPoints)
            {
                availablePoints -= _skillInspected.price;
                _skillInspected.unlocked = true;

                _skills.Remove(id);
                _skills.Add(id, _skillInspected);

                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}