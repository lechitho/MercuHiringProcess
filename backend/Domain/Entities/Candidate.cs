using Domain.Enum;
using Domain.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Candidate : Entity
{
    public string Name { get; private set; }
    public string Stage { get; private set; }
    public string Phone { get; private set; }
    public string Email { get; private set; }
    public Candidate(Guid id, string name, string stage, string phone, string email):base(id)
    {
        Name = name;
        Stage = stage;
        Phone = phone;
        Email = email;
    }
    
    public static Candidate Create(Guid id, string name, string stage, string phone, string email)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(email))
            throw new CandidateCannotCreatedException();

        stage = GetEnumValue(stage).ToString();
        return new Candidate(id, name, stage, phone, email);
    }
    private static Stage GetEnumValue(string value)
    {
        foreach (var field in typeof(Stage).GetFields())
        {
            if (Attribute.GetCustomAttribute(field, typeof(EnumMemberAttribute)) is EnumMemberAttribute attribute)
            {
                if (attribute.Value == value)
                {
                    return (Stage)field.GetValue(null);
                }
            }
        }
        return Domain.Enum.Stage.Applied;
    }
}
