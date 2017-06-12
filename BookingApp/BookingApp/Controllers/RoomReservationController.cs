﻿

using BookingApp.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace BookingApp.Controllers
{
    [RoutePrefix("api/RoomReservation")]
    public class RoomReservationController : ApiController
    {
        private BAContext db = new BAContext();

        [HttpGet]
        [Route("roomReservations", Name = "RoomReservationApi")]
        public IHttpActionResult GetRoomReservations()
        {
            DbSet<RoomReservations> roomReservations = db.AppRoomReservations;

            if (roomReservations == null)
            {
                return NotFound();
            }

            return Ok(roomReservations);

        }

        [HttpPut]
        [Route("roomReservations/{id}")]
        public IHttpActionResult PutRoomReservations(int id, RoomReservations roomReserv)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != roomReserv.Id)
            {
                return BadRequest("Ids are not matching!");
            }

            db.Entry(roomReserv).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.AppRoomReservations.Find(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [Route("roomReservations")]
        [ResponseType(typeof(RoomReservations))]
        public IHttpActionResult PostPlace(RoomReservations roomReservations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                db.AppRoomReservations.Add(roomReservations);

                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {

                throw;
            }

            return CreatedAtRoute("RoomReservationApi", new { id = roomReservations.Id }, roomReservations);
        }
    }
}