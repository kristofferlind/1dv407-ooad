#Workshop 3 - grade 5
Only change made to model is making card public, which was needed to present it's values. Changing view was pretty straightforward except for the issues presented below. We haven't learned .net mvc yet, which means we had to use webforms. But if we consider the codebehind a controller it still follows mvc pattern pretty well.

##Issues
Keeping observer pattern for individually updating cards was rather hard. This would have been much easier to solve by faking it with a css delay. To keep using the observer pattern we had to implement SignalR to push updates to the client and then update ui using javascript. Here we had to reimplent both scorekeeping and displaying of cards.

SignalR only works in chrome, we're guessing this isn't that important for the assignment and we don't really have time to figure out why.