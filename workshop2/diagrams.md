#Domain model
(http://yuml.me/b7a1a0e1)

[MemberRegister]
[Member|MemberId;Name;Social security number;]
[BoatManager]
[Boat|Length;Type]

[MemberRegister]++1-0..*[Member]
[Member]1-1[BoatManager]
[BoatManager]++1-0..*[Boat]

#Sequence diagrams
All sequence diagrams are in code for [https://www.websequencediagrams.com/#]


##Add member
(http://www.websequencediagrams.com/cgi-bin/cdraw?lz=TWVtYmVyVmlldy0-AAYGQ29udHJvbGxlcjpIYW5kbGVBZGQAIAYKABEQLT4rADYKOiBHZXRTdHJpbmdJbnB1dCgpACkHAFgFLT4tAE4RIHJldHVybiBOYW1lAARYU1NOAIEdGlJlZ2lzdGVyOiAAgVsJKE5hbWUsIFNTTgCBNwgAHQgAIxNHZXROZXcAgj0GSWQAgWgJACgKAIFtBwBeClIAgWwGACsIAEsYOiBuZXcAHwcAgQgKLAAvCQCCUwgtPitCb2F0TWFuYWcALggABgsoKQoAFAsAgnoKAIJxCWIAMwoASggAgSITAIMgBwCDeA0AggwKAII7EACETAZzLkFkZCgAhFgGAII5EQCDeRQ&s=default)

MemberView->MemberController:HandleAddMember
MemberController->+MemberView: GetStringInput()
MemberView-->-MemberController: return Name
MemberController->+MemberView: GetStringInput()
MemberView-->-MemberController: return SSN
MemberController->+MemberRegister: AddMember(Name, SSN)
MemberRegister->+MemberRegister: GetNewMemberId()
MemberRegister->-MemberRegister: Return MemberId
MemberRegister->+Member: new Member(Name, SSN, MemberId)
Member->+BoatManager: new BoatManager()
BoatManager-->-Member: return boatManager
Member-->-MemberRegister: return Member
MemberRegister->MemberRegister: Members.Add(Member)
MemberRegister-->-MemberController:


##List members
###Compact/Verbose (depending on showBoats:bool)
(http://www.websequencediagrams.com/cgi-bin/cdraw?lz=IyMjQ29tcGFjdC9WZXJib3NlIChkZXBlbmRpbmcgb24gc2hvd0JvYXRzOmJvb2wpCk1lbWJlclZpZXctPgAGBkNvbnRyb2xsZXI6SGFuZGxlAB0GcwAkBwAVCi0-KwA0CjogRGlzcGxheQBLBkxpc3QoAGMJAFkOAC8HUmVnaXN0ZXI6IEdldAAsCgCBCwcAFggtPi0AWQxSZXR1cm4gbQCBBg0AgTIMAIEHBQB6EUhlYWRlcgpsb29wADMJICAgIAB7HEdldEJvYXRNYW5hZ2VyKABtBi5JZCkALwsAgR0JAIEYD3IAgSEGYgA2CgBdEgBSCwCBfgVCb2F0AIIyBQBbBgBxCwBOEACBfgdib2F0AIE1EgCCdh1MaW5lAIF0BW9wdACEAQoAggYFACUjAIQzBSgAcgUAgX8GZW5kCmVuZACCXilGb290ZXIK&s=default)

MemberView->MemberController:HandleMembers
MemberController->+MemberView: DisplayMemberList(showBoats)
MemberView->+MemberRegister: GetMemberList
MemberRegister->-MemberView: Return members
MemberView->MemberView:DisplayMemberListHeader
loop members
    MemberView->+MemberRegister:GetBoatManager(member.Id)
    MemberRegister-->-MemberView: return boatManager
    MemberView->+BoatManager: GetBoatList()
    BoatManager-->-MemberView: Return boats
    MemberView->MemberView: DisplayMemberListLine
    opt showBoats
        MemberView->MemberView: DisplayBoats(boats)
    end
end
MemberView->MemberView:DisplayMemberListFooter



##Remove member
(http://www.websequencediagrams.com/cgi-bin/cdraw?lz=TWVtYmVyVmlldy0-AAYGQ29udHJvbGxlcjpIYW5kbGVSZW1vdmUAIwYKABQQLT4rADkKOiBHZXRDb25maXJtACMHAFUFLT4tAEsRIFJldHVybiAAKAc6Qm9vbApvcHQACQgKICAgIABVGVJlZ2lzdGVyOkRlbGV0AIEWByhtAIFIBUlkKQAyCwAiCACBVwgAMQkAgXQGcy4AgVkGADUHACUVAIErFAplbmQ&s=default)

MemberView->MemberController:HandleRemoveMember
MemberController->+MemberView: GetConfirm
MemberView-->-MemberController: Return Confirm:Bool
opt Confirm
    MemberController->+MemberRegister:DeleteMember(memberId)
    MemberRegister->MemberRegister:Members.Remove(member)
    MemberRegister-->-MemberController:
end


##Edit member
(http://www.websequencediagrams.com/cgi-bin/cdraw?lz=TWVtYmVyVmlldy0-AAYGQ29udHJvbGxlcjogSGFuZGxlRWRpdAAiBgoAExAtPisAOAo6IEdldFN0cmluZ0lucHV0KCkAKQcAWgUtPi0ATxJyZXR1cm4gTmFtZQAEWFNTTgCBHRo6VXBkYXQAdAgAgQ8VCg&s=default)

MemberView->MemberController: HandleEditMember
MemberController->+MemberView: GetStringInput()
MemberView-->-MemberController: return Name
MemberController->+MemberView: GetStringInput()
MemberView-->-MemberController: return SSN
MemberController->+Member:Update
Member-->-MemberController:


##View member
(http://www.websequencediagrams.com/cgi-bin/cdraw?lz=TWVtYmVyVmlldy0-AAYGQ29udHJvbGxlcjpIYW5kbGUAHQYKAA4QLT4rADMKOkRpc3BsYXkASQYobQBRBSkALQcAWAYAJwc6R2V0Qm9hdExpc3QoABoILS0-LQBCC1JldHVybiBib2F0cwpvcHQABAYuQ291bnQgPiAwCiAgICAAgSgSAIEEBSAAgQMHAFsJAEEFKQplbmQAgQMMAGgJAIFkCw&s=default)

MemberView->MemberController:HandleMember
MemberController->+MemberView:DisplayMember(member)
MemberView->+Member:GetBoatList()
Member-->-MemberView:Return boats
opt boats.Count > 0
    MemberView->MemberView: DisplayBoatList(boats)
end
MemberView-->-MemberController:


##Add boat to member
(http://www.websequencediagrams.com/cgi-bin/cdraw?lz=Qm9hdFZpZXctPk1lbWJlckNvbnRyb2xsZXI6SGFuZGxlQWRkQm9hdAoADxAtPisANAg6RGlzcGxheUJvYXRUeXBlTWVudQoAUAktPi0AShEAMR1HZXRJbnRlZ2VySW5wdXQALB5SZXR1cm4gYgB0BwA5IERvdWJsZQAqLkxlbmd0aACBaRhNYW5hZ2VyOgCCIAgACQsAgWYV&s=default)

BoatView->MemberController:HandleAddBoat
MemberController->+BoatView:DisplayBoatTypeMenu
BoatView-->-MemberController:
MemberController->+BoatView:GetIntegerInput
BoatView-->-MemberController:Return boatType
MemberController->+BoatView:GetDoubleInput
BoatView-->-MemberController:Return boatLength
MemberController->+BoatManager:AddBoat
BoatManager-->-MemberController:


##Remove members boat
(http://www.websequencediagrams.com/cgi-bin/cdraw?lz=Qm9hdFZpZXctPk1lbWJlckNvbnRyb2xsZXI6SGFuZGxlUmVtb3ZlQm9hdAoAEhAtPisANwg6R2V0Q29uZmlybQoASgktPi0ARBFSZXR1cm4gACUHOkJvb2wKb3B0AAkICiAgICAAVhJCb2F0TWFuYWcAPQUAgQgJICAgIAAQCwBcFQplbmQ&s=default)

BoatView->MemberController:HandleRemoveBoat
MemberController->+BoatView:GetConfirm
BoatView-->-MemberController:Return Confirm:Bool
opt Confirm
    MemberController->BoatManager:RemoveBoat
    BoatManager-->-MemberController:
end


##Edit members boat
(http://www.websequencediagrams.com/cgi-bin/cdraw?lz=Qm9hdFZpZXctPk1lbWJlckNvbnRyb2xsZXI6SGFuZGxlRWRpdEJvYXQoYm9hdCkKABYQLT4rADsIOkRpc3BsYXlCb2F0VHlwZU1lbnUKAFcJLT4tAFERADEdR2V0SW50ZWdlcklucHV0ACweUmV0dXJuIGIAdAcAOSBEb3VibGUAKi5MZW5ndGgAgWkYTWFuYWdlcjpVcGRhdGUAgigJLAAwCwAGBlR5cGUpAIISBQAsBwCCBRU&s=default)

BoatView->MemberController:HandleEditBoat(boat)
MemberController->+BoatView:DisplayBoatTypeMenu
BoatView-->-MemberController:
MemberController->+BoatView:GetIntegerInput
BoatView-->-MemberController:Return boatType
MemberController->+BoatView:GetDoubleInput
BoatView-->-MemberController:Return boatLength
MemberController->+BoatManager:UpdateBoat(boat, boatLength, boatType)
BoatManager-->-MemberController:

##Authentication
(http://www.websequencediagrams.com/cgi-bin/cdraw?lz=TWVtYmVyVmlldy0-AAYGQ29udHJvbGxlcjpIYW5kbGVBdXRoZW50aWNhdGlvbgoAFhAtPisAOwo6R2V0U3RyaW5nSW5wdXQAJgcAWgUtPi0AUBFSZXR1cm4gdXNlcm5hbWUACFRwYXNzd29yZACBHhpSZWdpc3RlcjoAgV4LAIEDCAAUCACBIxs&s=default)

MemberView->MemberController:HandleAuthentication
MemberController->+MemberView:GetStringInput
MemberView-->-MemberController:Return username
MemberController->+MemberView:GetStringInput
MemberView-->-MemberController:Return password
MemberController->+MemberRegister:Authenticate
MemberRegister-->-MemberController:Return

##Search for member
