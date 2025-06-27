<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="DeleteComment.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Comment.DeleteComment"
    meta:resourcekey="Page" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
    -
    <asp:Localize ID="Localize1" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <div id="form">
        <form id="DeleteCommentForm" method="post" runat="server">
            <br />
            <asp:Label ID="lblSureDelete"  meta:resourcekey="lblSureDelete" runat="server" ></asp:Label>
            <br />
            <div class="button">
                <asp:Button ID="btnDeleteComment" runat="server" OnClick="BtnDeleteCommentClick" meta:resourcekey="btnDeleteComment" />
            </div>
        </form>
    </div>
</asp:Content>