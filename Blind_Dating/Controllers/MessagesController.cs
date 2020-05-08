﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Blind_Dating.Models;
using Blind_Dating.ViewModels;

namespace Blind_Dating.Controllers
{
    public class MessagesController : Controller
    {
        private Blind_DatingContext _context;
        private UserManager<IdentityUser> _userManager;

        public MessagesController(Blind_DatingContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Inbox()
        {
            DatingProfile profile = _context.DatingProfile.FirstOrDefault(id =>
                id.UserAccountId == _userManager.GetUserId(User));

            InboxViewModel inbox = new InboxViewModel();
            inbox.mailMessages = _context.MailMessages.Where(to => to.ToProfileId == profile.Id).ToList();

            List<DatingProfile> fromList = new List<DatingProfile>();
            foreach (var msg in inbox.mailMessages)
            {
                fromList.Add(_context.DatingProfile.FirstOrDefault(from => from.Id == msg.FromProfileId));
            }

            inbox.fromProfiles = fromList;
            return View(inbox);
        }

        public IActionResult NewMessage(int id)
        {
            ViewBag.ToProfileId = id;
            return View();
        }

        public IActionResult Read(int id)
        {
            MailMessages mail = _context.MailMessages.FirstOrDefault(m => m.Id == id);
            mail.IsRead = true;
            _context.Update(mail);
            _context.SaveChanges();
            return View(mail);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Send([Bind("MessageTitle, MessageText")] MailMessages mail, int toProfileId)
        {
            DatingProfile fromUser = _context.DatingProfile.FirstOrDefault(p => p.UserAccountId == _userManager.GetUserId(User));
            mail.FromProfileId = fromUser.Id;
            mail.IsRead = false;
            mail.FromProfile = fromUser;
            mail.ToProfileId = toProfileId;

            _context.Add(mail);
            _context.SaveChanges();

            return RedirectToAction("Browse", "DatingProfiles");
        }
    }
}