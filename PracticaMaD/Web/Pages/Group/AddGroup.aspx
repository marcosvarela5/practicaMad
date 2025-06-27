<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="AddGroup.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Group.AddGroup" 
    meta:resourcekey="Page" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
    -
    <asp:Localize ID="Localize1" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <div id="form">
        <form id="AddGroupForm" method="post" runat="server">
            <h2><asp:Localize ID="Localize2" runat="server" meta:resourcekey="h2CreateGroup" /></h2>
            <asp:Label ID="lblName" runat="server" AssociatedControlID="txtName" meta:resourcekey="lblName"></asp:Label>
            <asp:TextBox ID="txtName" runat="server" meta:resourcekey="txtName"></asp:TextBox>
            <br />
            <asp:Label ID="lblDescription" runat="server" AssociatedControlID="txtDescription" meta:resourcekey="lblDescription"></asp:Label>
            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="2" meta:resourcekey="txtDescription"></asp:TextBox>
            <br />
            <asp:Label ID="lblCategory" runat="server" AssociatedControlID="ddlCategory" meta:resourcekey="lblCategory"></asp:Label>
            <asp:DropDownList ID="ddlCategory" runat="server" meta:resourcekey="ddlCategory"></asp:DropDownList>
            <br />
            <div class="button">
                <asp:Button ID="btnAddGroup" runat="server" OnClick="btnAddGroup_Click" meta:resourcekey="btnAddGroup" />
            </div>
            <br />
            <asp:Label ID="lblGroupCreated" runat="server" Visible="false" meta:resourcekey="lblGroupCreated"></asp:Label>
        </form>
    </div>
</asp:Content>