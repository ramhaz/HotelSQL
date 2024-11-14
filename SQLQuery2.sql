CREATE TABLE HotelFacility (
    Id int NOT NULL PRIMARY KEY,
    Price int NOT NULL,
    Hotel_No int NOT NULL,
    FacilityNo int NOT NULL,
    FOREIGN KEY (Hotel_No) REFERENCES DemoHotel(Hotel_No),
    FOREIGN KEY (FacilityNo) REFERENCES DemoFacility(FacilityNo)
);


