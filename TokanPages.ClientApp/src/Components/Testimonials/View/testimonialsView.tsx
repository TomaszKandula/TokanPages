import * as React from "react";
import { GET_IMAGES_URL } from "../../../Api";
import { Animated, Image, Collapsible, Skeleton, RenderHtml, Link, Icon } from "../../../Shared/Components";
import { TestimonialsViewProps } from "../Types";
import "./testimonialsView.css";

export const TestimonialsView = (props: TestimonialsViewProps): React.ReactElement => (
    <section className={props.className}>
        <div className="bulma-container">
            <div className="py-6">
                <Animated dataAos="fade-down">
                    <Skeleton isLoading={props.isLoading} height={40}>
                        <p className="is-size-3	has-text-centered has-text-link">{props.caption?.toUpperCase()}</p>
                    </Skeleton>
                </Animated>
                <div className="bulma-columns testimonials-card-columns">
                    <div className="bulma-column testimonials-card-column">
                        <Animated dataAos="fade-up" dataAosDelay={350}>
                            <div className="bulma-card">
                                <Skeleton isLoading={props.isLoading} className="testimonials-card-image">
                                    <Image
                                        base={GET_IMAGES_URL}
                                        source={props.Testimonial1.photo}
                                        className="testimonials-card-image"
                                        title="Testimonials"
                                        alt={`Picture of ${props.Testimonial1.name}`}
                                    />
                                </Skeleton>
                                <div className="bulma-card-content mt-6 testimonials-card-content">
                                    <Skeleton isLoading={props.isLoading} mode="Text" height={24} className="mt-6 pt-4">
                                        <h3 className="is-size-5 has-text-centered has-text-weight-semibold mt-6 px-4 pt-4 pb-1">
                                            {props.Testimonial1.name}
                                        </h3>
                                    </Skeleton>
                                    <Skeleton
                                        isLoading={props.isLoading}
                                        mode="Rect"
                                        height={24}
                                        width={24}
                                        disableMarginY
                                    >
                                        <div className="is-flex is-justify-content-center">
                                            <Link to={props.Testimonial1.link} aria-label={props.Testimonial1.name}>
                                                <figure className="bulma-image bulma-is-24x24">
                                                    <Icon name="LinkedIn" size={1.5} />
                                                </figure>
                                            </Link>
                                        </div>
                                    </Skeleton>
                                    <Skeleton isLoading={props.isLoading} mode="Text" height={24}>
                                        <p className="is-size-6 has-text-centered has-text-link p-2">
                                            {props.Testimonial1.occupation}
                                        </p>
                                    </Skeleton>
                                    <Skeleton isLoading={props.isLoading} height={100}>
                                        <Collapsible minHeight={120}>
                                            <RenderHtml
                                                value={props.Testimonial1.text}
                                                tag="p"
                                                className="is-size-6 has-text-centered has-text-grey p-2 line-height-18"
                                            />
                                        </Collapsible>
                                    </Skeleton>
                                </div>
                            </div>
                        </Animated>
                    </div>
                    <div className="bulma-column testimonials-card-column">
                        <Animated dataAos="fade-up" dataAosDelay={150}>
                            <div className="bulma-card">
                                <Skeleton isLoading={props.isLoading} className="testimonials-card-image">
                                    <Image
                                        base={GET_IMAGES_URL}
                                        source={props.Testimonial2.photo}
                                        className="testimonials-card-image"
                                        title="Testimonials"
                                        alt={`Picture of ${props.Testimonial2.name}`}
                                    />
                                </Skeleton>
                                <div className="bulma-card-content mt-6 testimonials-card-content">
                                    <Skeleton isLoading={props.isLoading} mode="Text" height={24} className="mt-6 p-4">
                                        <h3 className="is-size-5 has-text-centered has-text-weight-semibold mt-6 px-4 pt-4 pb-1">
                                            {props.Testimonial2.name}
                                        </h3>
                                    </Skeleton>
                                    <Skeleton
                                        isLoading={props.isLoading}
                                        mode="Rect"
                                        height={24}
                                        width={24}
                                        disableMarginY
                                    >
                                        <div className="is-flex is-justify-content-center">
                                            <Link to={props.Testimonial2.link} aria-label={props.Testimonial2.name}>
                                                <figure className="bulma-image bulma-is-24x24">
                                                    <Icon name="LinkedIn" size={1.5} />
                                                </figure>
                                            </Link>
                                        </div>
                                    </Skeleton>
                                    <Skeleton isLoading={props.isLoading} mode="Text" height={24}>
                                        <p className="is-size-6 has-text-centered has-text-link p-2">
                                            {props.Testimonial2.occupation}
                                        </p>
                                    </Skeleton>
                                    <Skeleton isLoading={props.isLoading} height={100}>
                                        <Collapsible minHeight={120}>
                                            <RenderHtml
                                                value={props.Testimonial2.text}
                                                tag="p"
                                                className="is-size-6 has-text-centered has-text-grey p-2 line-height-18"
                                            />
                                        </Collapsible>
                                    </Skeleton>
                                </div>
                            </div>
                        </Animated>
                    </div>
                    <div className="bulma-column testimonials-card-column">
                        <Animated dataAos="fade-up" dataAosDelay={250}>
                            <div className="bulma-card">
                                <Skeleton isLoading={props.isLoading} className="testimonials-card-image">
                                    <Image
                                        base={GET_IMAGES_URL}
                                        source={props.Testimonial3.photo}
                                        className="testimonials-card-image"
                                        title="Testimonials"
                                        alt={`Picture of ${props.Testimonial3.name}`}
                                    />
                                </Skeleton>
                                <div className="bulma-card-content mt-6 testimonials-card-content">
                                    <Skeleton isLoading={props.isLoading} mode="Text" height={24} className="mt-6 p-4">
                                        <h3 className="is-size-5 has-text-centered has-text-weight-semibold mt-6 px-4 pt-4 pb-1">
                                            {props.Testimonial3.name}
                                        </h3>
                                    </Skeleton>
                                    <Skeleton
                                        isLoading={props.isLoading}
                                        mode="Rect"
                                        height={24}
                                        width={24}
                                        disableMarginY
                                    >
                                        <div className="is-flex is-justify-content-center">
                                            <Link to={props.Testimonial3.link} aria-label={props.Testimonial3.name}>
                                                <figure className="bulma-image bulma-is-24x24">
                                                    <Icon name="LinkedIn" size={1.5} />
                                                </figure>
                                            </Link>
                                        </div>
                                    </Skeleton>
                                    <Skeleton isLoading={props.isLoading} mode="Text" height={24}>
                                        <p className="is-size-6 has-text-centered has-text-link p-2">
                                            {props.Testimonial3.occupation}
                                        </p>
                                    </Skeleton>
                                    <Skeleton isLoading={props.isLoading} height={100}>
                                        <Collapsible minHeight={120}>
                                            <RenderHtml
                                                value={props.Testimonial3.text}
                                                tag="p"
                                                className="is-size-6 has-text-centered has-text-grey p-2 line-height-18"
                                            />
                                        </Collapsible>
                                    </Skeleton>
                                </div>
                            </div>
                        </Animated>
                    </div>
                </div>
            </div>
        </div>
    </section>
);
