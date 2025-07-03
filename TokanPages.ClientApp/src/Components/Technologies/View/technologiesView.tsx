import * as React from "react";
import { useSelector } from "react-redux";
import { ApplicationState } from "../../../Store/Configuration";
import { Animated, Icon } from "../../../Shared/Components";

interface TechnologiesViewProps {
    className?: string;
}

export const TechnologiesView = (props: TechnologiesViewProps): React.ReactElement => {
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const technology = data?.components?.sectionTechnologies;

    return (
        <section className={`section ${props.className ?? ""}`}>
            <div className="bulma-container">
                <div className="py-6">
                    <Animated dataAos="fade-down">
                        <p className="is-size-3	has-text-centered has-text-link">
                            {technology?.caption?.toUpperCase()}
                        </p>
                    </Animated>
                    <div className="p-6">
                        <div className="bulma-columns ">
                            <div className="bulma-column">
                                <Animated dataAos="fade-up" className="is-flex is-align-items-center py-2">
                                    <Icon name="CodeBraces" size={1.2} className="has-text-link is-flex is-align-self-center mr-2" />
                                    <h3 className="is-size-4 py-5 has-text-black">
                                        {technology?.title1}
                                    </h3>
                                </Animated>
                                <Animated dataAos="fade-up">
                                    <h4 className="is-size-5 py-2 has-text-grey line-height-18">{technology?.text1}</h4>
                                </Animated>
                            </div>
                            <div className="bulma-column">
                                <Animated dataAos="fade-up" className="is-flex is-align-items-center py-2">
                                    <Icon name="Bookshelf" size={1.2} className="has-text-link is-flex is-align-self-center mr-2" />
                                    <h3 className="is-size-4 py-5 has-text-black">
                                        {technology?.title2}
                                    </h3>
                                </Animated>
                                <Animated dataAos="fade-up">
                                    <h4 className="is-size-5 py-2 has-text-grey line-height-18">{technology?.text2}</h4>
                                </Animated>
                            </div>
                        </div>
                        <div className="bulma-columns">
                            <div className="bulma-column">
                                <Animated dataAos="fade-up" className="is-flex is-align-items-center py-2">
                                    <Icon name="Server" size={1.2} className="has-text-link is-flex is-align-self-center mr-2" />
                                    <h3 className="is-size-4 py-5 has-text-black">
                                        {technology?.title3}
                                    </h3>
                                </Animated>
                                <Animated dataAos="fade-up">
                                    <h4 className="is-size-5 py-2 has-text-grey line-height-18">{technology?.text3}</h4>
                                </Animated>
                            </div>
                            <div className="bulma-column">
                                <Animated dataAos="fade-up" className="is-flex is-align-items-center py-2">
                                    <Icon name="Cloud" size={1.2} className="has-text-link is-flex is-align-self-center mr-2" />
                                    <h3 className="is-size-4 py-5 has-text-black">
                                        {technology?.title4}
                                    </h3>
                                </Animated>
                                <Animated dataAos="fade-up">
                                    <h4 className="is-size-5 py-2 has-text-grey line-height-18">{technology?.text4}</h4>
                                </Animated>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    );
};
