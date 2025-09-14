import React from "react";
import { GET_IMAGES_URL } from "../../../Api";
import { EducationItemProps, ExperienceItemProps, OccupationProps } from "../../../Api/Models";
import { CustomImage, Link, RenderList, Skeleton } from "../../../Shared/Components";
import { RenderCaptionProps, ResumeViewProps } from "../Types";
import { v4 as uuid } from "uuid";

const RenderExperienceList = (props: ResumeViewProps): React.ReactElement => (
    <>
        {props.content?.resume?.experience?.list.map((value: ExperienceItemProps, _index: number) => (
            <div key={uuid()} className="is-flex is-flex-direction-column mb-4">
                <div className="is-flex is-justify-content-space-between">
                    <div className="is-flex is-gap-1.5 my-2">
                        <Skeleton isLoading={props.isLoading} width={50} height={24}>
                            <p className="is-size-6 has-text-weight-bold has-text-grey-dark">{value.companyName}</p>
                        </Skeleton>
                        <Skeleton isLoading={props.isLoading} width={100} height={24}>
                            <p className="is-size-6 has-text-grey-dark">({value.contractType})</p>
                        </Skeleton>
                    </div>
                    <div className="is-flex is-gap-0.5 my-2">
                        <Skeleton isLoading={props.isLoading} width={100} height={24}>
                            <p className="is-size-6 has-text-grey-dark">{value.dateStart}</p>
                            <p className="is-size-6 has-text-grey-dark">-</p>
                            <p className="is-size-6 has-text-grey-dark">{value.dateEnd}</p>
                        </Skeleton>
                    </div>
                </div>
                {value.occupation.map((value: OccupationProps, _index: number) => (
                    <React.Fragment key={uuid()}>
                        <div className="is-flex is-gap-0.5">
                            <Skeleton isLoading={props.isLoading} width={250} height={24}>
                                <p className="is-size-6 has-text-grey-dark py-1">{value.name}</p>
                                <p className="is-size-6 has-text-grey-dark py-1">({value.dateStart}</p>
                                <p className="is-size-6 has-text-grey-dark py-1">-</p>
                                <p className="is-size-6 has-text-grey-dark py-1">{value.dateEnd})</p>
                            </Skeleton>
                        </div>
                        <div className="bulma-content mb-0">
                            <RenderList
                                isLoading={props.isLoading}
                                list={value.details}
                                className="is-size-6 has-text-grey-dark has-text-justified"
                            />
                        </div>
                    </React.Fragment>
                ))}
            </div>
        ))}
    </>
);

const RenderEducationList = (props: ResumeViewProps): React.ReactElement => (
    <>
        {props.content.resume.education.list.map((value: EducationItemProps, _index: number) => (
            <div key={uuid()} className="is-flex is-flex-direction-column mb-4">
                <div className="is-flex is-justify-content-space-between">
                    <div className="is-flex is-gap-1.5 my-2">
                        <Skeleton isLoading={props.isLoading} width={50} height={24}>
                            <p className="is-size-6 has-text-weight-bold has-text-grey-dark">{value.schoolName}</p>
                        </Skeleton>
                        <Skeleton isLoading={props.isLoading} width={100} height={24}>
                            <p className="is-size-6 has-text-grey-dark">({value.tenureInfo})</p>
                        </Skeleton>
                    </div>
                    <div className="is-flex is-gap-0.5 my-2">
                        <Skeleton isLoading={props.isLoading} width={100} height={24}>
                            <p className="is-size-6 has-text-grey-dark">{value.dateStart}</p>
                            <p className="is-size-6 has-text-grey-dark">-</p>
                            <p className="is-size-6 has-text-grey-dark">{value.dateEnd}</p>
                        </Skeleton>
                    </div>
                </div>
                <Skeleton isLoading={props.isLoading} width={300} height={24}>
                    <p className="is-size-6 has-text-grey-dark my-1">{value.details}</p>
                </Skeleton>
                <div className="is-flex is-gap-1.5">
                    <Skeleton isLoading={props.isLoading} height={24}>
                        <p className="is-size-6 has-text-grey-dark my-1">{value.thesis.label}:</p>
                        <Link to={`document?name=${value.thesis.file}`} className="is-size-6 my-1 is-underlined">
                            <>{value.thesis.name}</>
                        </Link>
                    </Skeleton>
                </div>
            </div>
        ))}
    </>
);

const RenderInterestsList = (props: ResumeViewProps): React.ReactElement => (
    <div className="is-flex my-4">
        {props.content.resume.interests.list.map((value: string, _index: number) => (
            <Skeleton isLoading={props.isLoading} height={24} key={uuid()} className="m-2">
                <p className="is-size-6 has-text-grey-dark">
                    {value},&nbsp;
                </p>
            </Skeleton>
        ))}
    </div>
);

const RenderCaption = (props: RenderCaptionProps): React.ReactElement => (
    <Skeleton isLoading={props.isLoading} mode="Text" width={200} height={24} hasSkeletonCentered className="my-4">
        <p className="is-size-4 has-text-grey-dark has-text-centered m-4">{props.text}</p>
    </Skeleton>
);

export const ResumeView = (props: ResumeViewProps): React.ReactElement => (
    <section className={props.className}>
        <div className="bulma-container bulma-is-max-tablet">
            <p className="is-size-3 is-uppercase has-text-grey-dark has-text-centered has-text-weight-light m-6">
                {props.content?.caption}
            </p>
            <div className="is-flex is-gap-2.5 mb-6">
                <div className="bulma-cell is-align-content-center">
                    <Skeleton isLoading={props.isLoading} mode="Circle" width={98} height={98} disableMarginY>
                        <figure className="bulma-image bulma-is-96x96">
                            <CustomImage
                                base={GET_IMAGES_URL}
                                source={props.content?.photo?.href}
                                title={props.content?.photo?.text}
                                alt={props.content?.photo?.text}
                                className="bulma-is-rounded"
                            />
                        </figure>
                    </Skeleton>
                </div>
                <div className="bulma-cell is-align-content-center">
                    <Skeleton isLoading={props.isLoading} mode="Text" width={100}>
                        <p className="is-size-6 has-text-grey-dark has-text-weight-bold is-capitalized">
                            {props.content?.resume?.header?.fullName}
                        </p>
                    </Skeleton>
                    <Skeleton isLoading={props.isLoading} mode="Text" width={100}>
                        <p className="is-size-6 has-text-grey-dark is-lowercase">
                            {props.content?.resume?.header?.mobilePhone}
                        </p>
                    </Skeleton>
                    <Skeleton isLoading={props.isLoading} mode="Text" width={100}>
                        <Link to={`mailto:${props.content?.resume?.header?.email}`} className="is-size-6 is-underlined">
                            <p className="is-lowercase">{props.content?.resume?.header?.email}</p>
                        </Link>
                    </Skeleton>
                    <Skeleton isLoading={props.isLoading} mode="Text" width={100}>
                        <Link to={props.content?.resume?.header?.github.href} className="is-size-6 is-underlined">
                            <p className="is-lowercase">{props.content?.resume?.header?.github.text}</p>
                        </Link>
                    </Skeleton>
                </div>
            </div>
            <RenderCaption isLoading={props.isLoading} text={props.content.resume.summary.caption} />
            <Skeleton isLoading={props.isLoading} mode="Text" height={24} className="my-4">
                <p className="is-size-6 has-text-grey-dark has-text-justified line-height-20">
                    {props.content.resume.summary.text}
                </p>
            </Skeleton>
            <RenderCaption isLoading={props.isLoading} text={props.content.resume.achievements.caption} />
            <div className="bulma-content has-text-grey-dark">
                <RenderList
                    isLoading={props.isLoading}
                    list={props.content.resume.achievements.list}
                    className="is-size-6 has-text-justified line-height-20"
                />
            </div>
            <RenderCaption isLoading={props.isLoading} text={props.content.resume.experience.caption} />
            <RenderExperienceList {...props} />
            <RenderCaption isLoading={props.isLoading} text={props.content.resume.education.caption} />
            <RenderEducationList {...props} />
            <RenderCaption isLoading={props.isLoading} text={props.content.resume.interests.caption} />
            <RenderInterestsList {...props} />
        </div>
    </section>
);
