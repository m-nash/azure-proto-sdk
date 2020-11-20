using Azure.ResourceManager.Authorization.Models;
using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_authorization
{
    public class PhRoleAssignment : Resource
    {
        public static azure_proto_core.ResourceType ResourceType => "Microsoft.Authorization/roleAssignments";

        RoleAssignment _model;
        public PhRoleAssignment(RoleAssignment assign)
        {
            _model = assign;
            Id = assign.Id;
            Scope = assign.Scope;
            RoleDefinitionId = assign.RoleDefinitionId;
            PrincipalId = assign.PrincipalId;
            PrincipalType = assign.PrincipalType;
            CanDelegate = assign.CanDelegate;
        }

        public string Scope { get; }

        public ResourceIdentifier RoleDefinitionId { get; }

        public string PrincipalId { get; }

        public PrincipalType? PrincipalType { get; }

        public bool? CanDelegate { get; }
        public override ResourceIdentifier Id { get ; protected set ; }

        public RoleAssignment ToModel()
        {
            return _model;
        }
    }
}
