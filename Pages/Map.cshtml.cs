using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace YourProjectName.Pages 
{
    public class Attraction
    {
        public string? Name { get; set; }
        public string? Subtitle { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string? Description { get; set; }
    }

    public class MapModel : PageModel
    {
        public List<Attraction>? Attractions { get; set; }

        public void OnGet()
        {
            Attractions = new List<Attraction>
            {
                new Attraction { 
                    Name = "Petronas Twin Towers", 
                    Subtitle = "KLCC", 
                    Lat = 3.1578, 
                    Lng = 101.7115, 
                    Description = "• **Origin & History:** Commissioned by the national oil company Petronas, construction began in 1993 and they officially opened in 1999, holding the title of the world's tallest buildings until 2004.<br>• **Key Features:** Features a stunning postmodern Islamic geometric design made of stainless steel and glass, with a famous Skybridge connecting the two structures.<br>• **Trivia & Facts:** The towers stand at 452 metres tall, were designed by renowned architect Cesar Pelli, and the Skybridge links them at the 41st and 42nd floors." 
                },
                new Attraction { 
                    Name = "Batu Caves", 
                    Subtitle = "Gombak", 
                    Lat = 3.2374, 
                    Lng = 101.6833, 
                    Description = "• **Origin & History:** The 400-million-year-old limestone caves were promoted as a place of worship in 1891 by K. Thamboosamy Pillay, who was inspired by the spear-like shape of the main cave entrance.<br>• **Key Features:** A majestic natural limestone outcrop featuring a series of massive caves packed with ornate shrines, vibrant paintwork, and native wild macaques.<br>• **Trivia & Facts:** The main shrine is dedicated to the Hindu deity Lord Murugan, whose giant golden statue stands 43 metres tall at the entrance, welcoming visitors to climb the 272 colourful steps. It attracts over a million people annually for the Thaipusam festival." 
                },
                new Attraction { 
                    Name = "Merdeka Square", 
                    Subtitle = "Dataran Merdeka", 
                    Lat = 3.1486, 
                    Lng = 101.6945, 
                    Description = "• **Origin & History:** Originally the Selangor Club Padang used heavily during British colonial rule, it was renamed Merdeka Square to commemorate the historic raising of the Malayan flag for independence on 31 August 1957.<br>• **Key Features:** A vast manicured civic lawn surrounded by historical heritage architecture, notably the iconic Moorish-style Sultan Abdul Samad Building.<br>• **Trivia & Facts:** The square boasts one of the world's tallest flagpoles. Long before independence, the field was primarily used to play cricket." 
                },
                new Attraction { 
                    Name = "Kuala Lumpur Tower", 
                    Subtitle = "Menara KL", 
                    Lat = 3.1528, 
                    Lng = 101.7038, 
                    Description = "• **Origin & History:** Conceived by the Malaysian government to elevate technological infrastructure, construction began in 1991, and it officially opened to the public in 1996.<br>• **Key Features:** A classic Islamic Muqarnas architecture dome design standing atop a protected nature reserve, complete with a revolving restaurant and a glass Sky Box.<br>• **Trivia & Facts:** Serving primarily as a telecommunications tower, it stands 421 metres tall on the forested hill of Bukit Nanas, and regularly hosts an international annual event for the extreme sport of BASE jumping." 
                },
                new Attraction { 
                    Name = "Central Market", 
                    Subtitle = "Pasar Seni", 
                    Lat = 3.1454, 
                    Lng = 101.6953, 
                    Description = "• **Origin & History:** Founded by Capitan Cina Yap Ah Loy, it originally opened in 1888 as a bustling wet market before being saved from demolition and transformed into a cultural landmark in 1985.<br>• **Key Features:** Divided into vibrant cultural zones showcasing traditional Malaysian art, the landmark stands out with its gorgeous pale blue Art Deco facade.<br>• **Trivia & Facts:** It sits incredibly close to Petaling Street (the heart of KL's Chinatown) and is best known today for retailing exquisite Malaysian arts, crafts, and unique souvenirs." 
                },
                new Attraction { 
                    Name = "Thean Hou Temple", 
                    Subtitle = "Robson Heights", 
                    Lat = 3.1216, 
                    Lng = 101.6877, 
                    Description = "• **Origin & History:** Built by the Malaysian Hainanese community and officially dedicated in 1989, it serves as a spiritual sanctuary and a lasting testament to Chinese architectural heritage.<br>• **Key Features:** A grand six-tiered structure combining modern structural design with elements of Buddhism, Taoism, and Confucianism, complete with elaborate carvings and red roofs.<br>• **Trivia & Facts:** The temple is dedicated to Mazu, the heavenly sea goddess, and is widely described as one of the largest Chinese temples in Southeast Asia. During Chinese New Year, it is famously covered in thousands of glowing red lanterns." 
                },
                new Attraction { 
                    Name = "Bukit Jalil National Stadium", 
                    Subtitle = "TM Stadium Nasional", 
                    Lat = 3.0548, 
                    Lng = 101.6915, 
                    Description = "• **Origin & History:** Constructed by the government specifically to elevate national sports capability and successfully host the 16th Commonwealth Games in 1998.<br>• **Key Features:** Features an iconic multi-tiered bowl configuration protected by a massive sloped membrane fabric roof, prioritizing wide sightlines.<br>• **Trivia & Facts:** Functioning as Malaysia's primary national stadium with a capacity exceeding 87,000 spectators, it serves as the home ground for the national football team and hosts premier global concert events." 
                },
                new Attraction { 
                    Name = "National Mosque of Malaysia", 
                    Subtitle = "Masjid Negara", 
                    Lat = 3.1415, 
                    Lng = 101.6915, 
                    Description = "• **Origin & History:** Completed in 1965 as an architectural monument dedicated to celebrating the peaceful achievement of the nation's independence without violence.<br>• **Key Features:** Designed with a striking 16-pointed star concrete main roof resembling an open umbrella, paired beautifully with a sleek 73-metre-high minaret.<br>• **Trivia & Facts:** The grand complex provides a tranquil sanctuary capable of holding up to 15,000 worshippers, surrounded by lush landscaped gardens." 
                },
                new Attraction { 
                    Name = "Bukit Bintang", 
                    Subtitle = "Shopping District", 
                    Lat = 3.1466, 
                    Lng = 101.7111, 
                    Description = "• **Origin & History:** Evolved over the late 20th century from a quiet colonial residential path into the modern commercial heartbeat of Kuala Lumpur.<br>• **Key Features:** A massive open-air destination home to multi-level upscale retail complexes, diverse street performances, and vibrant night paths.<br>• **Trivia & Facts:** Best known as the premier shopping and entertainment district of the city, it houses the luxury Pavilion KL mall and the famous Jalan Alor street food haven." 
                },
                new Attraction { 
                    Name = "Petaling Street", 
                    Subtitle = "Chinatown", 
                    Lat = 3.1440, 
                    Lng = 101.6976, 
                    Description = "• **Origin & History:** Originally a primary tapioca milling zone for early Chinese immigrants during the 19th-century tin mining boom, it evolved into the historic core of Chinatown.<br>• **Key Features:** A bustling open-air market sheltered by a grand green canopy roof, featuring traditional shop houses, historic temples, and deep cultural roots.<br>• **Trivia & Facts:** Widely celebrated for its unbeatable retail bargains, historical heritage, and legendary street food stalls, it sits adjacent to Central Market." 
                },
                new Attraction { 
                    Name = "KLCC Park", 
                    Subtitle = "Urban Sanctuary", 
                    Lat = 3.1556, 
                    Lng = 101.7150, 
                    Description = "• **Origin & History:** Opened alongside the Twin Towers in 1998, transforming what was once part of the historical Selangor Turf Club into a modern metropolitan park.<br>• **Key Features:** Spans 50 acres of meticulously designed parkland, integrating a massive man-made lake, a jogging track, and dense tropical greenery.<br>• **Trivia & Facts:** Designed by legendary Brazilian architect Roberto Burle Marx, it is famous for the striking Lake Symphony musical water fountain shows." 
                },
                new Attraction { 
                    Name = "Suria KLCC", 
                    Subtitle = "Premier Shopping", 
                    Lat = 3.1582, 
                    Lng = 101.7124, 
                    Description = "• **Origin & History:** Opened its doors in May 1998 as an essential commercial component of the comprehensive Kuala Lumpur City Centre mega-project.<br>• **Key Features:** A massive world-class shopping complex spanning six levels of retail space, offering an indoor science museum and an oceanarium.<br>• **Trivia & Facts:** Situated directly at the base of the Petronas Twin Towers, it stands out as one of Malaysia's most visited premier luxury retail spaces." 
                }
            };
        }
    }
}