using GymManager.Core.Members;
using GymManager.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GymManager.Core.Attendance;

namespace GymManager.AplicationServices.Members
{
    public class MembersAppService : IMembersAppService
    {
        private readonly IRepository<int, Member> _repository;

        public MembersAppService(IRepository<int, Member> repository)
        {
            _repository = repository;
        }

        public async Task<int> AddMemberAsync(Member member)
        {
            await _repository.AddAsync(member);

            return member.Id;
        }

        public async Task DeleteMemberAsync(int memberId)
        {
            await _repository.DeleteAsync(memberId);
        }

        public async Task EditMemberAsync(Member member)
        {
            await _repository.UpdateAsync(member);
        }

        public async Task<Member> GetMemberAsync(int memberId)
        {
            return await _repository.GetAsync(memberId);
        }

        public async Task<List<Member>> GetMembersAsync()
        {

            return await _repository.GetAll().ToListAsync();
        }

        public async Task<List<Member>> GetMembersAsync(int count)
        {
            List<Member> members = await _repository.GetAll().Include(x => x.MembershipType).Include(x => x.Checks).ToListAsync();
            

            List<MemberCheck> memberChecks = new List<MemberCheck>();

            MemberCheck memberCheck;

            int c = 0;

            foreach (Member member in members)
            {
                c++;
                memberCheck = new MemberCheck();
                memberCheck.Member = member;
                memberCheck.Count = 0;
                memberCheck.Id = c;

                foreach (Check check in member.Checks)
                {
                    if (check.DateCheck >= DateTime.Now.AddDays(-30))
                    {
                        memberCheck.Count++;
                    }
                }

                if (memberCheck.Count > 0)
                {
                    memberChecks.Add(memberCheck);
                }

            }

            memberChecks = memberChecks.OrderByDescending(x => x.Count).ToList();

            members.Clear();

            if (count > memberChecks.Count)
            {
                count = memberChecks.Count;
            }

            for (int i = 0; i < count; i++)
            {
                members.Add(memberChecks.ElementAt(i).Member);
            }

            return members;
        }

        public async Task<List<Member>> GetMembersFilterAsync(string filter)
        {
            if (filter == null)
            {
                return await _repository.GetAll().ToListAsync();
            } else
            {
              return await _repository.GetAll().Where(x => x.Name.Contains(filter)).ToListAsync();
            }
        }

        
    }
}
