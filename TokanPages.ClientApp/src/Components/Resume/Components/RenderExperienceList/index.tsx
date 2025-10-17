import React from "react";
import { OccupationProps } from "../../../../Api/Models";
import { RenderList, Skeleton } from "../../../../Shared/Components";
import { ProcessedExperienceItemProps, ResumeViewProps } from "../../Types";
import { ProcessTimeSpan } from "../../Utilities";
import { RenderCompanyLink } from "../RenderCompanyLink";
import { RenderTags } from "../RenderTags";
import { v4 as uuid } from "uuid";

export const RenderExperienceList = (props: ResumeViewProps): React.ReactElement => (
    <>
        {props.processed.map((value: ProcessedExperienceItemProps, _index: number) => (
            <div key={uuid()} className="is-flex is-flex-direction-column mb-4">
                <div className="is-flex is-justify-content-space-between is-align-items-center">
                    <div className="is-flex is-flex-direction-column my-3">
                        <div className="is-flex is-align-items-center is-gap-1.5">
                            <Skeleton isLoading={props.isLoading} width={50} height={24}>
                                <p className="is-size-5 has-text-weight-bold has-text-grey-dark">{value.companyName}</p>
                            </Skeleton>
                            <Skeleton isLoading={props.isLoading} width={24} height={24}>
                                <RenderCompanyLink {...value} />
                            </Skeleton>
                        </div>
                        <Skeleton isLoading={props.isLoading} width={100} height={24}>
                            <p className="is-size-5 has-text-grey">{value.contractType}</p>
                        </Skeleton>
                    </div>
                    <div className="is-flex is-flex-direction-column my-3">
                        <Skeleton isLoading={props.isLoading} width={100} height={24}>
                            <p className="is-size-5 has-text-right has-text-grey-dark is-lowercase">
                                {value.dateStart} - {value.dateEnd}
                            </p>
                            <p className="is-size-5 has-text-right has-text-grey is-lowercase">
                                <ProcessTimeSpan
                                    months={value.timespan}
                                    yearLabel={props.page?.translations?.singular?.yearLabel}
                                    monthLabel={props.page?.translations?.singular?.monthLabel}
                                    yearsLabel={props.page?.translations?.plural?.yearsLabel}
                                    monthsLabel={props.page?.translations?.plural?.monthsLabel}
                                />
                            </p>
                        </Skeleton>
                    </div>
                </div>
                {value.occupation.map((value: OccupationProps, _index: number) => (
                    <React.Fragment key={uuid()}>
                        <div className="is-flex is-flex-direction-column my-2">
                            <Skeleton isLoading={props.isLoading} width={250} height={24}>
                                <p className="is-size-5 has-text-grey-dark py-0">{value.name}</p>
                                <p className="is-size-5 has-text-grey py-0 is-lowercase">
                                    {value.dateStart} - {value.dateEnd}
                                </p>
                            </Skeleton>
                        </div>
                        <div className="bulma-content mb-1">
                            <RenderList
                                isLoading={props.isLoading}
                                list={value.details}
                                className="is-size-5 has-text-grey-dark"
                            />
                            <Skeleton isLoading={props.isLoading}>
                                <RenderTags {...value} />
                            </Skeleton>
                        </div>
                    </React.Fragment>
                ))}
            </div>
        ))}
    </>
);
