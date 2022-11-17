using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Calabonga.StateManager.ConsoleTest.Entities;
using Calabonga.StateManager.ConsoleTest.Start;
using Calabonga.StateManager.ConsoleTest.Statuses;
using Calabonga.StatusProcessor;

namespace Calabonga.StateManager.ConsoleTest
{

    class Program
    {

        static void Main(string[] args)
        {

            var listOfStatus = new List<IEntityStatus>
            {
                new StateFree(),
                new StateBinded(),
                new StateArrivedAtPlace(),
                new StateCompleted(),
                new StateDeleted(),
                new StateTemporarily()
            };


            using (var container = DependencyContainer.Initialize())
            {
                {
                    var processor = container.Resolve<AccidentRuleProcessor>();

                    var entity = processor.Create();

#if DBTEST

                using (var db = new ApplicationDbContext()) {
                    db.Accidents.Add(entity);
                    db.SaveChanges();

                    var entity1 = db.Accidents.Find(entity.Id);
                if (entity1 != null)
                {
                    processor.UpdateStatus(entity1, StateBinded.Guid);
                        db.SaveChanges();
                    }
                }

#endif
                    Console.WriteLine($"Rules count: {processor.Rules.Count()}");


                    ChangeStatus(StateBinded.Guid, entity, processor);
                    processor.UpdateStatus(entity, StateArrivedAtPlace.Guid);

                    ChangeStatus(StateFree.Guid, entity, processor);



                    Console.ReadLine();
                }
            }
        }

        static void ChangeStatus(Guid status, Accident entity, AccidentRuleProcessor processor) {
            Console.WriteLine("-------------------- changing ------------------------");
            Console.WriteLine($"Current status: {entity.EntityActiveStatus} : {processor.ToEnum<AccidentStatus>(entity.EntityActiveStatus).Result}");
            var result = processor.UpdateStatus(entity, status);
            if (result.IsOk) {
                Console.WriteLine($"Requested status: {processor.ToEnum<AccidentStatus>(status).Result}. Current status is: {processor.ToEnum<AccidentStatus>(entity.EntityActiveStatus).Result}");
            }
            else {
                Console.WriteLine("Changing status errors: ");
                foreach (var error in result.Errors.ToList()) {
                    Console.WriteLine("\t - {0}", error);
                }
            }
            Console.WriteLine("--------------------- changed ------------------------");
            Console.WriteLine($"Changing result: {result.IsOk}");
            Console.WriteLine("");
        }
    }
}
