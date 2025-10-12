import React from "react";
import { GET_IMAGES_URL } from "../../../Api";
import { EducationItemProps, OccupationProps } from "../../../Api/Models";
import { CustomImage, Icon, Link, Media, RenderList, Skeleton } from "../../../Shared/Components";
import { RenderTag } from "../../../Shared/Components/RenderContent/Renderers";
import { ProcessedExperienceItemProps, RenderCaptionProps, ResumeViewProps } from "../Types";
import { ProcessTimeSpan } from "../Utilities";
import { v4 as uuid } from "uuid";
import Validate from "validate.js";

const RenderCompanyLink = (props: ProcessedExperienceItemProps): React.ReactElement =>
    Validate.isEmpty(props.companyLink) ? (
        <Icon name="OpenInNew" size={1.2} className="has-text-grey" />
    ) : (
        <Link to={props.companyLink}>
            <Icon name="OpenInNew" size={1.2} className="has-text-link is-clickable" />
        </Link>
    );

const RenderCaption = (props: RenderCaptionProps): React.ReactElement => (
    <Skeleton isLoading={props.isLoading} mode="Text" width={200} height={24} hasSkeletonCentered className="my-4">
        <RenderTag tag={props.tag ?? "p"} className="is-size-4 has-text-grey-dark has-text-centered is-uppercase p-5">
            <>{props.text}</>
        </RenderTag>
    </Skeleton>
);

const RenderTags = (props: OccupationProps): React.ReactElement =>
    Validate.isEmpty(props.tags) ? (
        <></>
    ) : (
        <div className="bulma-tags pt-2 ml-3 mb-4">
            {props.tags?.map((item: string, _index: number) => (
                <div className="bulma-tag bulma-is-medium" key={uuid()}>
                    {item}
                </div>
            ))}
        </div>
    );

const RenderExperienceList = (props: ResumeViewProps): React.ReactElement => (
    <>
        {props.processed.map((value: ProcessedExperienceItemProps, _index: number) => (
            <div key={uuid()} className="is-flex is-flex-direction-column mb-4">
                <div className="is-flex is-justify-content-space-between is-align-items-center">
                    <div className="is-flex is-flex-direction-column my-3">
                        <div className="is-flex is-align-items-center is-gap-1.5">
                            <Skeleton isLoading={props.isLoading} width={50} height={24}>
                                <p className="is-size-5 has-text-weight-bold has-text-grey-dark">{value.companyName}</p>
                            </Skeleton>
                            <RenderCompanyLink {...value} />
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
                            <RenderTags {...value} />
                        </div>
                    </React.Fragment>
                ))}
            </div>
        ))}
    </>
);

const RenderEducationList = (props: ResumeViewProps): React.ReactElement => (
    <>
        {props.page.resume.education.list.map((value: EducationItemProps, _index: number) => (
            <div key={uuid()} className="is-flex is-flex-direction-column mb-4">
                <div className="is-flex is-justify-content-space-between is-align-items-center">
                    <div className="is-flex is-flex-direction-column my-2">
                        <Skeleton isLoading={props.isLoading} width={50} height={24}>
                            <p className="is-size-5 has-text-weight-bold has-text-grey-dark">{value.schoolName}</p>
                        </Skeleton>
                        <Skeleton isLoading={props.isLoading} width={100} height={24}>
                            <p className="is-size-5 has-text-grey">{value.tenureInfo}</p>
                        </Skeleton>
                    </div>
                    <Skeleton isLoading={props.isLoading} width={100} height={24}>
                        <p className="is-size-5 has-text-grey-dark">
                            {value.dateStart} - {value.dateEnd}
                        </p>
                    </Skeleton>
                </div>
                <Skeleton isLoading={props.isLoading} width={300} height={24}>
                    <p className="is-size-5 has-text-grey-dark my-1">{value.details}</p>
                </Skeleton>
                <div className="is-flex is-gap-1.5">
                    <Skeleton isLoading={props.isLoading} height={24}>
                        <p className="is-size-5 has-text-grey-dark my-1">{value.thesis.label}:</p>
                        <Link
                            to={`/${props.languageId}/document?name=${value.thesis.file}&redirect=resume`}
                            className="is-size-5 my-1 is-underlined"
                        >
                            <>{value.thesis.name}</>
                        </Link>
                    </Skeleton>
                </div>
            </div>
        ))}
    </>
);

const RenderInterestsList = (props: ResumeViewProps): React.ReactElement => (
    <div className="bulma-tags mt-4 mb-6">
        <Skeleton isLoading={props.isLoading} height={24} className="m-2">
            {props.page.resume.interests.list.map((value: string, _index: number) => (
                <span key={uuid()} className="bulma-tag bulma-is-medium bulma-is-info bulma-is-light">
                    {value}
                </span>
            ))}
        </Skeleton>
    </div>
);

const RenderTestimonials = (props: ResumeViewProps) => (
    <div className="bulma-content is-size-5 has-text-grey-dark">
        <div className="mt-5 mb-6">
            <Skeleton isLoading={props.isLoading} mode="Rect" height={150}>
                <blockquote className="is-italic has-text-justified has-background-white line-height-20 m-0">
                    {props.section.text1}
                </blockquote>
            </Skeleton>
            <div className="is-flex is-justify-content-flex-end is-align-items-center is-gap-1.5">
                <div className="has-text-right my-4">
                    <Skeleton isLoading={props.isLoading} width={150} height={24}>
                        <Link to={props.section.linkedIn1} className="is-underlined">
                            <>{props.section.name1}</>
                        </Link>
                    </Skeleton>
                    <Skeleton isLoading={props.isLoading} height={24} width={150}>
                        <p className="m-0">{props.section.occupation1}</p>
                    </Skeleton>
                </div>
                <Skeleton isLoading={props.isLoading} mode="Circle" width={64} height={64}>
                    <figure className="bulma-image bulma-is-64x64">
                        <CustomImage
                            base={GET_IMAGES_URL}
                            source={props.section.photo1}
                            title={props.section.name1}
                            alt={props.section.name1}
                            className="bulma-is-rounded is-round-border-light"
                        />
                    </figure>
                </Skeleton>
            </div>
        </div>
        <div className="mb-6">
            <Skeleton isLoading={props.isLoading} mode="Rect" height={150}>
                <blockquote className="is-italic has-text-justified has-background-white line-height-20 m-0">
                    {props.section.text2}
                </blockquote>
            </Skeleton>
            <div className="is-flex is-justify-content-flex-end is-align-items-center is-gap-1.5">
                <div className="has-text-right my-4">
                    <Skeleton isLoading={props.isLoading} width={150} height={24}>
                        <Link to={props.section.linkedIn2} className="is-underlined">
                            <>{props.section.name2}</>
                        </Link>
                    </Skeleton>
                    <Skeleton isLoading={props.isLoading} width={150} height={24}>
                        <p className="m-0">{props.section.occupation2}</p>
                    </Skeleton>
                </div>
                <Skeleton isLoading={props.isLoading} mode="Circle" width={64} height={64}>
                    <figure className="bulma-image bulma-is-64x64">
                        <CustomImage
                            base={GET_IMAGES_URL}
                            source={props.section.photo2}
                            title={props.section.name2}
                            alt={props.section.name2}
                            className="bulma-is-rounded is-round-border-light"
                        />
                    </figure>
                </Skeleton>
            </div>
        </div>
        <div className="mb-6">
            <Skeleton isLoading={props.isLoading} mode="Rect" height={150}>
                <blockquote className="is-italic has-text-justified has-background-white line-height-20 m-0">
                    {props.section.text3}
                </blockquote>
            </Skeleton>
            <div className="is-flex is-justify-content-flex-end is-align-items-center is-gap-1.5">
                <div className="has-text-right my-4">
                    <Skeleton isLoading={props.isLoading} width={150} height={24}>
                        <Link to={props.section.linkedIn3} className="is-underlined">
                            <>{props.section.name3}</>
                        </Link>
                    </Skeleton>
                    <Skeleton isLoading={props.isLoading} width={150} height={24}>
                        <p className="m-0">{props.section.occupation3}</p>
                    </Skeleton>
                </div>
                <Skeleton isLoading={props.isLoading} mode="Circle" width={64} height={64}>
                    <figure className="bulma-image bulma-is-64x64">
                        <CustomImage
                            base={GET_IMAGES_URL}
                            source={props.section.photo3}
                            title={props.section.name3}
                            alt={props.section.name3}
                            className="bulma-is-rounded is-round-border-light"
                        />
                    </figure>
                </Skeleton>
            </div>
        </div>
    </div>
);

const RenderResume = (props: ResumeViewProps) => (
    <>
        <h2 className="is-size-3 is-uppercase has-text-grey-dark has-text-centered has-text-weight-light m-5">
            {props.page?.caption}
        </h2>
        <div className="is-flex is-gap-2.5 mb-4">
            <div className="bulma-cell is-align-content-center">
                <Skeleton isLoading={props.isLoading} mode="Circle" width={98} height={98} disableMarginY>
                    <figure className="bulma-image bulma-is-128x128">
                        <CustomImage
                            base={GET_IMAGES_URL}
                            source={props.page?.photo?.href}
                            title={props.page?.photo?.text}
                            alt={props.page?.photo?.text}
                            className="bulma-is-rounded is-round-border-light"
                        />
                    </figure>
                </Skeleton>
            </div>
            <div className="bulma-cell is-align-content-center">
                <Skeleton isLoading={props.isLoading} mode="Text" width={100}>
                    <p className="is-size-4 has-text-grey-dark has-text-weight-bold is-capitalized">
                        {props.page?.resume?.header?.fullName}
                    </p>
                </Skeleton>
                <Skeleton isLoading={props.isLoading} mode="Text" width={100}>
                    <p className="is-size-6 has-text-grey-dark is-lowercase">
                        {props.page?.resume?.header?.mobilePhone}
                    </p>
                </Skeleton>
                <Skeleton isLoading={props.isLoading} mode="Text" width={100}>
                    <Link to={`mailto:${props.page?.resume?.header?.email}`} className="is-size-6 is-underlined">
                        <p className="is-lowercase">{props.page?.resume?.header?.email}</p>
                    </Link>
                </Skeleton>
                <Skeleton isLoading={props.isLoading} mode="Text" width={100}>
                    <Link to={props.page?.resume?.header?.github.href} className="is-size-6 is-underlined">
                        <p className="is-lowercase">{props.page?.resume?.header?.github.text}</p>
                    </Link>
                </Skeleton>
            </div>
        </div>
        <RenderCaption isLoading={props.isLoading} text={props.page.resume.summary.caption} tag="h3" />
        <Skeleton isLoading={props.isLoading} mode="Text" height={24} className="my-4">
            <h4 className="is-size-5 has-text-grey-dark has-text-justified line-height-20">
                {props.page.resume.summary.text}
            </h4>
        </Skeleton>
        <RenderCaption isLoading={props.isLoading} text={props.page.resume.achievements.caption} />
        <div className="bulma-content has-text-grey-dark">
            <RenderList
                isLoading={props.isLoading}
                list={props.page.resume.achievements.list}
                className="is-size-5 has-text-justified line-height-20"
            />
        </div>
        <RenderCaption isLoading={props.isLoading} text={props.page.resume.experience.caption} />
        <RenderExperienceList {...props} />
        <RenderCaption isLoading={props.isLoading} text={props.page.resume.education.caption} />
        <RenderEducationList {...props} />
        <RenderCaption isLoading={props.isLoading} text={props.section.caption} />
        <RenderTestimonials {...props} />
        <RenderCaption isLoading={props.isLoading} text={props.page.resume.interests.caption} />
        <RenderInterestsList {...props} />
    </>
);

export const ResumeView = (props: ResumeViewProps): React.ReactElement => (
    <section className={props.className}>
        <div className="bulma-container bulma-is-max-tablet pb-6">
            <Media.DesktopOnly>
                <RenderResume {...props} />
            </Media.DesktopOnly>
            <Media.TabletOnly>
                <div className="mx-4">
                    <RenderResume {...props} />
                </div>
            </Media.TabletOnly>
            <Media.MobileOnly>
                <div className="mx-4">
                    <RenderResume {...props} />
                </div>
            </Media.MobileOnly>
        </div>
    </section>
);
