using ElevenNote.Models;
using ElevenNote.Servies;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using AuthorizeAttribute = System.Web.Mvc.AuthorizeAttribute;
using RoutePrefixAttribute = System.Web.Http.RoutePrefixAttribute;

namespace ElevenNote.WebMVC.Controllers.WebAPI
{
    [Authorize]
    [RoutePrefix("api/Note")]
    public class NoteController : ApiController
    {
        private bool SetStarState(int noteId, bool newState)
        {
            // Create the service
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new NoteService(userId);

            // Get the note
            var detail = service.GetNoteById(noteId);

            // Create the NoteEdit model instance with the new star state
            var updatedNote =
                new NoteEdit
                {
                    NoteId = detail.NoteId,
                    Title = detail.Title,
                    Content = detail.Content,
                    IsStarred = newState
                };

            // Return a value indicating whether the update succeeded
            return service.UpdateNote(updatedNote);
        }

        [System.Web.Http.Route("{id}/Star")]
        [System.Web.Http.HttpPut]
        public bool ToggleStarOn(int id) => SetStarState(id, true);

        [System.Web.Http.Route("{id}/Star")]
        [System.Web.Http.HttpDelete]
        public bool ToggleStarOff(int id) => SetStarState(id, false);
    }
}