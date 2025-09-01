import * as React from "react";
import { useSelector } from "react-redux";
import { ApplicationState } from "../../../Store/Configuration";
import { Animated, Icon, RenderHtml, Skeleton } from "../../../Shared/Components";
import "./technologiesView.css";

interface TechnologiesViewProps {
    className?: string;
}

export const TechnologiesView = (props: TechnologiesViewProps): React.ReactElement => {
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const isLoading = data?.isLoading;
    const technology = data?.components?.sectionTechnologies;

    return (
        <section className={props.className}>
            <div className="bulma-container">
                <div className="py-6">
                    <Animated dataAos="fade-down">
                        <Skeleton isLoading={isLoading} mode="Text" height={40}>
                            <p className="is-size-3	has-text-centered has-text-link">
                                {technology?.caption?.toUpperCase()}
                            </p>
                        </Skeleton>
                    </Animated>
                    <div className="technologies-view-margins">
                        <div className="bulma-columns">
                            <div className="bulma-column">
                                <Animated dataAos="fade-up" className="is-flex is-align-items-center py-2">
                                    <Skeleton isLoading={isLoading} mode="Circle" width={40} height={40}>
                                        <Icon
                                            name="CodeBraces"
                                            size={1.2}
                                            className="has-text-link is-flex is-align-self-center mr-2"
                                        />
                                    </Skeleton>
                                    <Skeleton isLoading={isLoading} mode="Text" width={200} className="ml-2">
                                        <h3 className="is-size-4 py-5 has-text-black">{technology?.title1}</h3>
                                    </Skeleton>
                                </Animated>
                                <Animated dataAos="fade-up">
                                    <Skeleton isLoading={isLoading} mode="Text" height={100}>
                                        <RenderHtml
                                            value={technology?.text1}
                                            tag="p"
                                            className="is-size-5 py-2 has-text-grey line-height-18"
                                        />
                                    </Skeleton>
                                </Animated>
                            </div>
                            <div className="bulma-column">
                                <Animated dataAos="fade-up" className="is-flex is-align-items-center py-2">
                                    <Skeleton isLoading={isLoading} mode="Circle" width={40} height={40}>
                                        <Icon
                                            name="Bookshelf"
                                            size={1.2}
                                            className="has-text-link is-flex is-align-self-center mr-2"
                                        />
                                    </Skeleton>
                                    <Skeleton isLoading={isLoading} mode="Text" width={200} className="ml-2">
                                        <h3 className="is-size-4 py-5 has-text-black">{technology?.title2}</h3>
                                    </Skeleton>
                                </Animated>
                                <Animated dataAos="fade-up">
                                    <Skeleton isLoading={isLoading} mode="Text" height={100}>
                                        <RenderHtml
                                            value={technology?.text2}
                                            tag="p"
                                            className="is-size-5 py-2 has-text-grey line-height-18"
                                        />
                                    </Skeleton>
                                </Animated>
                            </div>
                        </div>
                        <div className="bulma-columns">
                            <div className="bulma-column">
                                <Animated dataAos="fade-up" className="is-flex is-align-items-center py-2">
                                    <Skeleton isLoading={isLoading} mode="Circle" width={40} height={40}>
                                        <Icon
                                            name="Server"
                                            size={1.2}
                                            className="has-text-link is-flex is-align-self-center mr-2"
                                        />
                                    </Skeleton>
                                    <Skeleton isLoading={isLoading} mode="Text" width={200} className="ml-2">
                                        <h3 className="is-size-4 py-5 has-text-black">{technology?.title3}</h3>
                                    </Skeleton>
                                </Animated>
                                <Animated dataAos="fade-up">
                                    <Skeleton isLoading={isLoading} mode="Text" height={100}>
                                        <RenderHtml
                                            value={technology?.text3}
                                            tag="p"
                                            className="is-size-5 py-2 has-text-grey line-height-18"
                                        />
                                    </Skeleton>
                                </Animated>
                            </div>
                            <div className="bulma-column">
                                <Animated dataAos="fade-up" className="is-flex is-align-items-center py-2">
                                    <Skeleton isLoading={isLoading} mode="Circle" width={40} height={40}>
                                        <Icon
                                            name="Cloud"
                                            size={1.2}
                                            className="has-text-link is-flex is-align-self-center mr-2"
                                        />
                                    </Skeleton>
                                    <Skeleton isLoading={isLoading} mode="Text" width={200} className="ml-2">
                                        <h3 className="is-size-4 py-5 has-text-black">{technology?.title4}</h3>
                                    </Skeleton>
                                </Animated>
                                <Animated dataAos="fade-up">
                                    <Skeleton isLoading={isLoading} mode="Text" height={100}>
                                        <RenderHtml
                                            value={technology?.text4}
                                            tag="p"
                                            className="is-size-5 py-2 has-text-grey line-height-18"
                                        />
                                    </Skeleton>
                                </Animated>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    );
};
