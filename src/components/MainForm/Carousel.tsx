import React from 'react';
import { Swiper, SwiperSlide } from 'swiper/react';
// import { Navigation, Pagination, Autoplay } from 'swiper/modules';
import 'swiper/swiper-bundle.css';

// Импортируем необходимые модули Swiper
import 'swiper/css/navigation';
import 'swiper/css/pagination';
import 'swiper/css/autoplay';

interface SlideProps {
  imageUrl: string;
  caption: string;
  link: string;
}

interface CarouselProps {
  slides: SlideProps[];
}

export const Carousel: React.FC<CarouselProps> = ({ slides }) => {
  return (
    <div>
        <p>hello</p>
    <Swiper
      spaceBetween={30}
      slidesPerView={1}
      navigation
      pagination={{ clickable: true }}
      autoplay={{ delay: 5000, disableOnInteraction: false }}
    >
      {slides.map((slide, index) => (
        <SwiperSlide key={index}>
          <a href={slide.link} target="_blank" rel="noopener noreferrer">
            <img src={slide.imageUrl} alt={slide.caption} />
            <p>{slide.caption}</p>
          </a>
        </SwiperSlide>
      ))}
    </Swiper>
    </div>

  );
};