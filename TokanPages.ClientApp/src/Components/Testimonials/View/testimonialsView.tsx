import * as React from "react";
import { GET_TESTIMONIALS_URL } from "../../../Api";
import { ViewProperties } from "../../../Shared/Abstractions";
import { Animated, CustomImage, Collapsible, Skeleton } from "../../../Shared/Components";
import "./testimonialsView.css";

interface TestimonialsViewProps extends ViewProperties {
    isMobile: boolean;
    caption: string;
    subtitle: string;
    photo1: string;
    name1: string;
    occupation1: string;
    text1: string;
    photo2: string;
    name2: string;
    occupation2: string;
    text2: string;
    photo3: string;
    name3: string;
    occupation3: string;
    text3: string;
    className?: string;
}

export const TestimonialsView = (props: TestimonialsViewProps): React.ReactElement => (
    <section className={props.className}>
        <div className="bulma-container">
            <div className="py-6">
                <Animated dataAos="fade-down">
                    <Skeleton isLoading={props.isLoading} height={40}>
                        <p className="is-size-3	has-text-centered has-text-link">{props.caption?.toUpperCase()}</p>
                    </Skeleton>
                </Animated>
                <div className={`bulma-columns ${props.isMobile ? "p-4" : "p-6"}`}>
                    <div className={`bulma-column ${props.isMobile ? "mt-6" : ""}`}>
                        <Animated dataAos="fade-up" dataAosDelay={350}>
                            <div className="bulma-card">
                                <Skeleton isLoading={props.isLoading} className="testimonials-card-image">
                                    <CustomImage
                                        base={GET_TESTIMONIALS_URL}
                                        source={props.photo1}
                                        className="testimonials-card-image"
                                        title="Testimonials"
                                        alt={`Picture of ${props.name1}`}
                                    />
                                </Skeleton>
                                <div className="bulma-card-content mt-6">
                                    <Skeleton isLoading={props.isLoading} mode="Text" height={24} className="mt-6 pt-4">
                                        <p className="is-size-4 has-text-centered has-text-weight-semibold mt-6 p-4">
                                            {props.name1}
                                        </p>
                                    </Skeleton>
                                    <Skeleton isLoading={props.isLoading} mode="Text" height={24}>
                                        <p className="is-size-6 has-text-centered has-text-link p-2">
                                            {props.occupation1}
                                        </p>
                                    </Skeleton>
                                    <Skeleton isLoading={props.isLoading} height={100}>
                                        <Collapsible minHeight={120}>
                                            <h4 className="is-size-6 has-text-centered has-text-grey p-2 line-height-18">
                                                {props.text1}
                                            </h4>
                                        </Collapsible>
                                    </Skeleton>
                                </div>
                            </div>
                        </Animated>
                    </div>
                    <div className={`bulma-column ${props.isMobile ? "mt-6" : ""}`}>
                        <Animated dataAos="fade-up" dataAosDelay={150}>
                            <div className="bulma-card">
                                <Skeleton isLoading={props.isLoading} className="testimonials-card-image">
                                    <CustomImage
                                        base={GET_TESTIMONIALS_URL}
                                        source={props.photo2}
                                        className="testimonials-card-image"
                                        title="Testimonials"
                                        alt={`Picture of ${props.name2}`}
                                    />
                                </Skeleton>
                                <div className="bulma-card-content mt-6">
                                    <Skeleton isLoading={props.isLoading} mode="Text" height={24} className="mt-6 p-4">
                                        <p className="is-size-4 has-text-centered has-text-weight-semibold mt-6 p-4">
                                            {props.name2}
                                        </p>
                                    </Skeleton>
                                    <Skeleton isLoading={props.isLoading} mode="Text" height={24}>
                                        <p className="is-size-6 has-text-centered has-text-link p-2">
                                            {props.occupation2}
                                        </p>
                                    </Skeleton>
                                    <Skeleton isLoading={props.isLoading} height={100}>
                                        <Collapsible minHeight={120}>
                                            <h4 className="is-size-6 has-text-centered has-text-grey p-2 line-height-18">
                                                {props.text2}
                                            </h4>
                                        </Collapsible>
                                    </Skeleton>
                                </div>
                            </div>
                        </Animated>
                    </div>
                    <div className={`bulma-column ${props.isMobile ? "mt-6" : ""}`}>
                        <Animated dataAos="fade-up" dataAosDelay={250}>
                            <div className="bulma-card">
                                <Skeleton isLoading={props.isLoading} className="testimonials-card-image">
                                    <CustomImage
                                        base={GET_TESTIMONIALS_URL}
                                        source={props.photo3}
                                        className="testimonials-card-image"
                                        title="Testimonials"
                                        alt={`Picture of ${props.name3}`}
                                    />
                                </Skeleton>
                                <div className="bulma-card-content mt-6">
                                    <Skeleton isLoading={props.isLoading} mode="Text" height={24} className="mt-6 p-4">
                                        <p className="is-size-4 has-text-centered has-text-weight-semibold mt-6 p-4">
                                            {props.name3}
                                        </p>
                                    </Skeleton>
                                    <Skeleton isLoading={props.isLoading} mode="Text" height={24}>
                                        <p className="is-size-6 has-text-centered has-text-link p-2">
                                            {props.occupation3}
                                        </p>
                                    </Skeleton>
                                    <Skeleton isLoading={props.isLoading} height={100}>
                                        <Collapsible minHeight={120}>
                                            <h4 className="is-size-6 has-text-centered has-text-grey p-2 line-height-18">
                                                {props.text3}
                                            </h4>
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
