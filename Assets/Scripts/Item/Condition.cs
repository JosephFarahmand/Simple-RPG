using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "New Condition", menuName = "Condition")]
public class Condition : ScriptableObject
{
    [SerializeField] private Type type;
    [SerializeField, Label("Operator"), ShowIf(nameof(isDefault))] private Operator @operator;
    [SerializeField, Label("Operator"), ShowIf(nameof(type), Type.ItemCount)] private ItemOperator itemOperator;
    [SerializeField, Label("Operator"), ShowIf(nameof(type), Type.String)] private StringOperator stringOperator;

    bool isDefault
    {
        get
        {
            switch (type)
            {
                case Type.ItemCount:
                    return false;
                case Type.String:
                    return false;
                default:
                    return true;
            }
        }
    }
    [Header("Value")]
    [SerializeField, ShowIf(nameof(type), Type.ItemCount), Dropdown(nameof(GetItems))] private string itemId;
    [SerializeField, ShowIf(nameof(type), Type.ItemCount)] private int itemCount;
    [Header("Value"), SerializeField, Label("Value"), ShowIf(nameof(type), Type.String)] private string stringValue;
    [Header("Value"), SerializeField, Label("Value"), ShowIf(nameof(type), Type.Intiger)] private int intValue;
    [Header("Value"), SerializeField, Label("Value"), ShowIf(nameof(type), Type.Float)] private float floatValue;
    [Header("Value"), SerializeField, Label("Value"), ShowIf(nameof(type), Type.Boolean)] private bool boolValue;

    public bool Check(string value)
    {
        if (type != Type.String)
        {
            Debug.LogWarning("Not defined for this type!!");
            return false;
        }

        switch (stringOperator)
        {
            case StringOperator.Equal:
                return stringValue.Equals(value);
            case StringOperator.NotEqual:
                return !stringValue.Equals(value);
            default:
                Debug.LogWarning("No operator defined!!");
                return false;
        }
    }

    public bool Check(int value)
    {
        if (type != Type.Intiger)
        {
            Debug.LogWarning("Not defined for this type!!");
            return false;
        }

        switch (@operator)
        {
            case Operator.Equal:
                return value == intValue;
            case Operator.NotEqual:
                return value != intValue;
            case Operator.LessThan:
                return value < intValue;
            case Operator.LessEqual:
                return value <= intValue;
            case Operator.GraderEqual:
                return value >= intValue;
            case Operator.GraderThan:
                return value > intValue;
            default:
                Debug.LogWarning("No operator defined!!");
                return false;
        }
    }

    public bool Check(Item value)
    {
        if (type != Type.Intiger)
        {
            Debug.LogWarning("Not defined for this type!!");
            return false;
        }

        if (itemId != value.Id)
        {
            Debug.LogWarning("Not defined for this type!!");
            return false;
        }

        switch (itemOperator)
        {
            case ItemOperator.Equal:
                return value.Count == itemCount;
            case ItemOperator.AtLeast:
                return value.Count >= itemCount;
            default:
                Debug.LogWarning("No operator defined!!");
                return false;
        }

        //switch (@operator)
        //{
        //    case Operator.Equal:
        //        return value.Count == itemCount;
        //    case Operator.NotEqual:
        //        return value.Count != itemCount;
        //    case Operator.LessThan:
        //        return value.Count < itemCount;
        //    case Operator.LessEqual:
        //        return value.Count <= itemCount;
        //    case Operator.GraderEqual:
        //        return value.Count >= itemCount;
        //    case Operator.GraderThan:
        //        return value.Count > itemCount;
        //    default:
        //        Debug.LogWarning("No operator defined!!");
        //        return false;
        //}
    }


    private DropdownList<string> GetItems()
    {
        var resault = new DropdownList<string>();
#if UNITY_EDITOR
        var all = Resources.FindObjectsOfTypeAll(typeof(Item)) as Item[];
        if (all.Length == 0)
        {
            resault.Add("Not found!", "");
        }
        else
        {
            foreach (var obj in all)
            {
                resault.Add(obj.Name, obj.Id);
            }
        }
#endif
        return resault;
    }

    public enum Type
    {
        String,
        Intiger,
        Float,
        Boolean,
        ItemCount
    }

    public enum StringOperator
    {
        Equal,
        NotEqual
    }

    public enum ItemOperator
    {
        Equal,
        AtLeast
    }

    public enum Operator
    {
        Equal,
        NotEqual,
        GraderThan,
        GraderEqual,
        LessThan,
        LessEqual
    }
}