import * as React from 'react';
import * as ReactDOM from 'react-dom';
import Slider from 'react-slick';

module App {
    const htmlView = document.querySelector('.react-carousel');

    class ReactCarousel extends React.Component {
        render() {
            const settings = {
                dots: true,
                infinite: true,
                speed: 500,
                slidesToShow: 1,
                slidesToScroll: 1
            };
            return (
                <div>
                    <h1>Hello</h1>
                    <Slider {...settings}>
                        <div><h3>1</h3></div>
                        <div><h3>2</h3></div>
                        <div><h3>3</h3></div>
                        <div><h3>4</h3></div>
                        <div><h3>5</h3></div>
                        <div><h3>6</h3></div>
                    </Slider>
                </div>
            );
        }
    }

    ReactDOM.render(<ReactCarousel />, htmlView);
}