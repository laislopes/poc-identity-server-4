CREATE TABLE Profiles_Events(
	ProfileId int FOREIGN KEY REFERENCES Profiles(Id),
	EventId int FOREIGN KEY REFERENCES Events(Id),
	PRIMARY KEY(ProfileId, EventId)
)
