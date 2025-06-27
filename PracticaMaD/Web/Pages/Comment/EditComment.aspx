<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="EditComment.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Comment.EditComment"
    meta:resourcekey="Page" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
    -
    <asp:Localize ID="Localize1" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <div id="form">
        <form id="EditCommentForm" method="post" runat="server">
            <br />
            <asp:TextBox ID="txtComment" runat="server" Width="300px" Columns="35" meta:resourcekey="txtCommentResource1"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvComment" runat="server" ControlToValidate="txtComment"
                Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>" meta:resourcekey="rfvCommentResource1"></asp:RequiredFieldValidator>
            <br />
            <div class="button">
                <asp:Button ID="btnEditComment" runat="server" OnClick="BtnEditCommentClick" meta:resourcekey="btnEditComment" />
            </div>
        </form>
    </div>
    <br />
    <asp:Label ID="lblNotOwner" Text="<%$ Resources:lblNotOwner %>" runat="server" Style="color: red" meta:resourcekey="lblNotOwner"></asp:Label>
    <asp:Label ID="lblCommentOkey" Text="<%$ Resources:lblCommentOkey %>" runat="server" Style="color: green" meta:resourcekey="lblCommentOkey"></asp:Label>
    <br />
</asp:Content>