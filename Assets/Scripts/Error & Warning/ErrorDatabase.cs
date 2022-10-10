using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "new Error Database", menuName = "Error Database", order = 1)]
public class ErrorDatabase : ScriptableObject
{
    [SerializeField] private List<ErrorEntity> errors;

    public ErrorDatabase()
    {
    }

    public ErrorEntity GetError(int code)
    {
        return errors.Find(e => e.Code == code);
    }

    [Serializable]
    public struct ErrorEntity
    {
        [SerializeField] ErrorType errorType;
        [SerializeField] private int code;
        [SerializeField] private string title;
        [SerializeField,NaughtyAttributes.ResizableTextArea] private string message;

        public ErrorEntity(ErrorType errorType, int code, string title, string message)
        {
            this.errorType = errorType;
            this.code = code;
            this.title = title;
            this.message = message;
        }

        //public ErrorEntity():this()
        //{
        //    errorType = ErrorType.Error;
        //    code = 0;
        //    title = string.Empty;
        //    message = string.Empty;
        //}

        //[SerializeField] private string description;

        public int Code => code;

        public ErrorType ErrorType { get => errorType; }
        public string Title { get => title;  }
        public string Message { get => message;  }
    }

    public enum ErrorType
    {
        Error,
        Warning,
        Message,
        Hint
    }
}
