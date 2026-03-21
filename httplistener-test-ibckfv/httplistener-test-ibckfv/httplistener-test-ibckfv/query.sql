CREATE TABLE IF NOT EXISTS users (
                                     id SERIAL PRIMARY KEY,
                                     login TEXT NOT NULL UNIQUE,
                                     password_hash TEXT NOT NULL,
                                     full_name TEXT,
                                     role TEXT NOT NULL CHECK(role IN ('Admin','User')),
                                     session_id TEXT
);

CREATE TABLE IF NOT EXISTS cities (
                                      id SERIAL PRIMARY KEY,
                                      name TEXT NOT NULL UNIQUE
);

CREATE UNIQUE INDEX IF NOT EXISTS idx_cities_name_lower ON cities (lower(name));

CREATE TABLE IF NOT EXISTS apartments (
                                          id SERIAL PRIMARY KEY,
                                          title TEXT NOT NULL,
                                          description TEXT,
                                          price_per_day TEXT NOT NULL,
                                          owner_id INTEGER NOT NULL REFERENCES users(id),
                                          city_id INTEGER NOT NULL REFERENCES cities(id),
                                          created_at TIMESTAMP WITH TIME ZONE DEFAULT now()
);

CREATE TABLE IF NOT EXISTS bookings (
                                        id SERIAL PRIMARY KEY,
                                        apartment_id INTEGER NOT NULL REFERENCES apartments(id),
                                        user_id INTEGER NOT NULL REFERENCES users(id),
                                        date_start DATE NOT NULL,
                                        date_end DATE NOT NULL,
                                        status TEXT NOT NULL CHECK(status IN ('Pending','Approved','Rejected'))
);

CREATE TABLE IF NOT EXISTS reviews (
                                       id SERIAL PRIMARY KEY,
                                       apartment_id INTEGER NOT NULL REFERENCES apartments(id),
                                       user_id INTEGER NOT NULL REFERENCES users(id),
                                       rating INTEGER NOT NULL CHECK (rating BETWEEN 1 AND 5),
                                       comment TEXT,
                                       created_at TIMESTAMP WITH TIME ZONE DEFAULT now()
);

INSERT INTO Users (login, password_hash, full_name, role, session_id)
VALUES (
           'admin',
           '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9',
           'System Administrator',
           'Admin',
           NULL
       );