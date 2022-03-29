A work in progress EventPlanner which has a unique use-case and isn't your generic agenda. I'm kidding, I'm talking bullshit. This is just a dummy project to get myself into C# with no intentions for real use. Feel free to review my code to tell me what I could have done differently, on a technical C# level!

## Todos
- [x] Change private method convention to CamelCase

- [x] Add virtual folders (src/tests)

- [x] Integration Tests
  - Refactor get occasions by given range since there's no range!

- [x] Handling null values (e.g. in repo)

- [x] Add filters (error handling, includes integration tests)

- [x] Refactor Id param in /occasions/:id/invitations

- [x] Introduce Fixture to generate data for tests

- [x] Refactor to have an aggregate

- [x] inject http context in presenters
- 
- [ ] List vs Collection vs Array vs IEnumerable (https://www.claudiobernasconi.ch/2013/07/22/when-to-use-ienumerable-icollection-ilist-and-list/)

- [ ] When to use In and Out, when to use init;

- [ ] Records

- [ ] Look into usage of enums, what about mapping to strings?

- [ ] Add facade for use-cases

- [ ] AutoMapper, maybe.

- [ ] Receiver should be a value object

- [ ] Move id generation to infra
 
- [ ] Add a time to an Occasion, value object for day and time.

- [ ] Reset invitations each week and resend them.

- [ ] Allow user to set a re-occuring reply (e.g. accept occasion at all times)

- [ ] Add real email service

- [ ] Replace in memory with psql driver

- [ ] Setup configuration (for example system email)

- [ ] Look into deployment

- [ ] Add authentication

- [ ] Make sure code is consistent

- [ ] Clean up problem details

- [ ] Solve TODOs in code if there are any
