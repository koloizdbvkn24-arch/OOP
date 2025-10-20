using System;
using System.Reflection;
namespace lab2_15
{
    class Song
    {
        private string title = string.Empty;
        private string artist = string.Empty;

        public string Title
        {
            get { return title; }
            set
            {
                if (value == null || value.Length < 3)
                {
                    Console.WriteLine("Назва пісні має мати більш ніж 3 символи");
                }
                else
                {
                    title = value;
                }
            }
        }

        public string Artist
        {
            get { return artist; }
            set
            {
                if (value == null || value.Length < 2)
                {
                    Console.WriteLine("Назва виконавця має мати більш ніж 2 символи");
                }
                else
                {
                    artist = value;
                }
            }
        }

        public Song(string T, string A)
        {
            Title = T;
            Artist = A;
        }

        public override string ToString()
        {
            return $"{title} - {artist}";
        }
    }
    class Playlist
    {
        private List<Song> songs = new List<Song>();

        public int Length => songs.Count;

        public Song this[int index]
        {
            get
            {
                if (index < 0 || index >= songs.Count)
                {
                    throw new IndexOutOfRangeException("Індекс виходить за межі списку");
                }
                return songs[index];
            }
            set
            {
                if (index < 0 || index >= songs.Count)
                {
                    throw new IndexOutOfRangeException("Індекс виходить за межі списку");
                }
                songs[index] = value;
            }
        }

        public Song this[string title]
        {
            get
            {
                foreach (var song in songs)
                {
                    if (song.Title == title)
                    {
                        return song;
                    }
                }
                throw new ArgumentException("Пісня з такою назвою не знайдена");
            }
        }

        public static Playlist operator +(Playlist p, Song s)
        {
            p.songs.Add(s);
            return p;
        }

        public static Playlist operator -(Playlist p, int index)
        {
            if (index < 0 || index >= p.songs.Count)
            {
                throw new IndexOutOfRangeException("Індекс виходить за межі списку");
            }
            p.songs.RemoveAt(index);
            return p;
            
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Playlist playlist = new Playlist();
            Song song1 = new Song("Come as You Are", "Nirvana");
            Song song2 = new Song("Billie Jean", "Michael Jackson");
            Song song3 = new Song("Smells Like Teen Spirit", "Nirvana");
            playlist += song1;
            playlist += song2;
            playlist += song3;

            for (int i = 0; i < playlist.Length; i++)
            {
                Console.WriteLine($"Пісня {i + 1}: {playlist[i]}");
            }

            Console.WriteLine($"Було видаленно пісню {playlist[2]}");
            playlist -= 2;

            for (int i = 0; i < playlist.Length; i++)
            {
                Console.WriteLine($"Пісня {i + 1}: {playlist[i]}");
            }

            Console.WriteLine($"Довжина плейлиста: {playlist.Length}");

        }
    }
}