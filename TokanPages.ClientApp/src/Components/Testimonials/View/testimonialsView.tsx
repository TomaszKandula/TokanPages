import * as React from "react";
import { GET_TESTIMONIALS_URL } from "../../../Api";
import { ViewProperties } from "../../../Shared/Abstractions";
import { Animated, CustomImage } from "../../../Shared/Components";
import { Collapsible } from "../../../Shared/Components";
import "./testimonialsView.css";

interface TestimonialsViewProps extends ViewProperties {
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
    background?: string;
}

export const TestimonialsView = (props: TestimonialsViewProps): React.ReactElement => (
        <section className={`section ${props.background ?? ""}`}>
            <div className="bulma-container">
                <div className="py-6">
                    <Animated dataAos="fade-down">
                        <p className="is-size-3	has-text-centered has-text-link">
                            {props.caption?.toUpperCase()}
                        </p>
                    </Animated>
                    <div className="bulma-columns p-6">
                        <div className="bulma-column">
                            <Animated dataAos="fade-up" dataAosDelay={350}>
                                <div className="bulma-card">
                                    <CustomImage
                                        base={GET_TESTIMONIALS_URL}
                                        source={props.photo1}
                                        className="testimonials-card-image"
                                        title="Testimonials"
                                        alt={`Picture of ${props.name1}`}
                                    />
                                    <div className="bulma-card-content mt-6">
                                        <p className="is-size-4 has-text-centered has-text-weight-semibold mt-6 p-4">
                                            {props.name1}
                                        </p>
                                        <p className="is-size-6 has-text-centered has-text-link p-2">
                                            {props.occupation1}
                                        </p>
                                        <Collapsible minHeight={120}>
                                            <h4 className="is-size-6 has-text-centered has-text-grey p-2 line-height-18">
                                                {props.text1}
                                            </h4>
                                        </Collapsible>
                                    </div>
                                </div>
                            </Animated>
                        </div>
                        <div className="bulma-column">
                            <Animated dataAos="fade-up" dataAosDelay={150}>
                                <div className="bulma-card">
                                    <CustomImage
                                        base={GET_TESTIMONIALS_URL}
                                        source={props.photo2}
                                        className="testimonials-card-image"
                                        title="Testimonials"
                                        alt={`Picture of ${props.name2}`}
                                    />
                                    <div className="bulma-card-content mt-6">
                                        <p className="is-size-4 has-text-centered has-text-weight-semibold mt-6 p-4">
                                            {props.name2}
                                        </p>
                                        <p className="is-size-6 has-text-centered has-text-link p-2">
                                            {props.occupation2}
                                        </p>
                                        <Collapsible minHeight={120}>
                                            <h4 className="is-size-6 has-text-centered has-text-grey p-2 line-height-18">
                                                {props.text2}
                                            </h4>
                                        </Collapsible>
                                    </div>
                                </div>
                            </Animated>
                        </div>
                        <div className="bulma-column">
                            <Animated dataAos="fade-up" dataAosDelay={250}>
                                <div className="bulma-card">
                                    <CustomImage
                                        base={GET_TESTIMONIALS_URL}
                                        source={props.photo3}
                                        className="testimonials-card-image"
                                        title="Testimonials"
                                        alt={`Picture of ${props.name3}`}
                                    />
                                    <div className="bulma-card-content mt-6">
                                        <p className="is-size-4 has-text-centered has-text-weight-semibold mt-6 p-4">
                                            {props.name3}
                                        </p>
                                        <p className="is-size-6 has-text-centered has-text-link p-2">
                                            {props.occupation3}
                                        </p>
                                        <Collapsible minHeight={120}>
                                            <h4 className="is-size-6 has-text-centered has-text-grey p-2 line-height-18">
                                                {props.text3}
                                            </h4>
                                        </Collapsible>
                                    </div>
                                </div>
                            </Animated>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    );

