CREATE TABLE users (
    user_id SERIAL PRIMARY KEY,
    name TEXT NOT NULL,
    email TEXT UNIQUE NOT NULL
);

CREATE TABLE posts (
    post_id SERIAL PRIMARY KEY,
    user_id INTEGER NOT NULL REFERENCES users(user_id) ON DELETE CASCADE,
    title TEXT NOT NULL,
    content TEXT,
    created_at TIMESTAMP DEFAULT NOW()
);

CREATE TABLE tags (
    tag_id SERIAL PRIMARY KEY,
    name TEXT NOT NULL
);

CREATE TABLE post_tags (
    post_id INTEGER NOT NULL REFERENCES posts(post_id) ON DELETE CASCADE,
    tag_id INTEGER NOT NULL REFERENCES tags(tag_id) ON DELETE CASCADE,
    PRIMARY KEY (post_id, tag_id)
);

CREATE OR REPLACE FUNCTION get_post_count_by_user(uid INTEGER)
    RETURNS INTEGER AS $$
DECLARE
    cnt INTEGER;
BEGIN
    SELECT COUNT(*) INTO cnt FROM posts WHERE user_id = uid;
    RETURN cnt;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE PROCEDURE get_posts_by_user(IN uid INTEGER)
    LANGUAGE SQL
AS $$
SELECT post_id, title, content, created_at
FROM posts
WHERE user_id = uid
ORDER BY created_at DESC;
$$;
