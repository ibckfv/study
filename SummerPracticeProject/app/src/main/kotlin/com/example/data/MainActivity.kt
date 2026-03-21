package com.example.data

import android.os.Bundle
import android.util.Log
import androidx.activity.ComponentActivity
import androidx.lifecycle.lifecycleScope
import com.example.data.AppDatabase
import com.example.data.User
import kotlinx.coroutines.launch

class MainActivity : ComponentActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        val db = AppDatabase.getDatabase(applicationContext)
        val userDao = db.userDao()

        lifecycleScope.launch {
            val user1 = User(name = "Иван", email = "ivan@example.com", password = "1234")
            userDao.insert(user1)
            val user2 = User(name = "Дамир", email = "damir@gmail.com", password = "1234")
            userDao.insert(user2)
            val user3 = User(name = "Ринат", email = "rinat@gmail.com", password = "1234")
            userDao.insert(user3)
            val loaded1 = userDao.getUserByEmail("ivan@example.com")
            Log.d("ROOM_TEST", "Загруженный пользователь: $loaded1")
            val loaded2 = userDao.getUserById(4)
            Log.d("ROOM_TEST", "Загруженный пользователь: $loaded2")
            val loaded3 = userDao.getUserByEmail("fghfgh")
            Log.d("ROOM_TEST", "Загруженный пользователь: $loaded3")
        }
    }
}