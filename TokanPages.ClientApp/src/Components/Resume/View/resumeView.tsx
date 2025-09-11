import React from "react";
import { GET_IMAGES_URL } from "../../../Api";
import { EducationItemProps, ExperienceItemProps, OccupationProps } from "../../../Api/Models";
import { CustomImage, RenderList, Skeleton } from "../../../Shared/Components";
import { ResumeViewProps } from "../Types";
import { v4 as uuid } from "uuid";

const RenderExperienceList = (props: ResumeViewProps): React.ReactElement => (
    <>
        {props.content?.resume?.experience?.list.map((value: ExperienceItemProps, _index: number) => (
            <div key={uuid()} className="is-flex is-flex-direction-column mb-4">
                <div className="is-flex is-justify-content-space-between">
                    <div className="is-flex is-gap-1.5 my-2">
                        <p className="is-size-6 has-text-weight-bold has-text-grey-dark">{value.companyName}</p>
                        <p className="is-size-6 has-text-grey-dark">({value.contractType})</p>
                    </div>
                    <div className="is-flex is-gap-0.5 my-2">
                        <p className="is-size-6 has-text-grey-dark">{value.dateStart}</p>
                        <p className="is-size-6 has-text-grey-dark">-</p>
                        <p className="is-size-6 has-text-grey-dark">{value.dateEnd}</p>
                    </div>
                </div>
                {value.occupation.map((value: OccupationProps, _index: number) => (
                    <React.Fragment key={uuid()}>
                        <div className="is-flex is-gap-0.5">
                            <p className="is-size-6 has-text-grey-dark py-1">{value.name}</p>
                            <p className="is-size-6 has-text-grey-dark py-1">({value.dateStart}</p>
                            <p className="is-size-6 has-text-grey-dark py-1">-</p>
                            <p className="is-size-6 has-text-grey-dark py-1">{value.dateEnd})</p>
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
                        <p className="is-size-6 has-text-weight-bold has-text-grey-dark">{value.schoolName}</p>
                        <p className="is-size-6 has-text-grey-dark">({value.tenureInfo})</p>
                    </div>
                    <div className="is-flex is-gap-0.5 my-2">
                        <p className="is-size-6 has-text-grey-dark">{value.dateStart}</p>
                        <p className="is-size-6 has-text-grey-dark">-</p>
                        <p className="is-size-6 has-text-grey-dark">{value.dateEnd}</p>
                    </div>
                </div>
                <p className="is-size-6 has-text-grey-dark my-1">{value.details}</p>
                <p className="is-size-6 has-text-grey-dark my-1">{value.thesis.name}</p>
            </div>
        ))}
    </>
);

const RenderInterestsList = (props: ResumeViewProps): React.ReactElement => (
    <div className="is-flex my-4">
        {props.content.resume.interests.list.map((value: string, _index: number) => (
            <p key={uuid()} className="is-size-6 has-text-grey-dark">
                {value},&nbsp;
            </p>
        ))}
    </div>
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
                        <p className="is-size-6 has-text-grey-dark is-lowercase">{props.content?.resume?.header?.mobilePhone}</p>
                    </Skeleton>
                    <Skeleton isLoading={props.isLoading} mode="Text" width={100}>
                        <p className="is-size-6 has-text-grey-dark is-lowercase">{props.content?.resume?.header?.email}</p>
                    </Skeleton>
                    <Skeleton isLoading={props.isLoading} mode="Text" width={100}>
                        <p className="is-size-6 has-text-grey-dark is-lowercase">{props.content?.resume?.header?.github.text}</p>
                    </Skeleton>
                    {/* <Skeleton isLoading={props.isLoading} mode="Text" width={100}>
                        <p className="is-size-6 has-text-grey-dark">{props.content?.resume?.header?.www.text}</p>
                    </Skeleton> */}
                </div>
            </div>
            <Skeleton
                isLoading={props.isLoading}
                mode="Text"
                width={200}
                height={24}
                hasSkeletonCentered
                className="my-4"
            >
                <p className="is-size-4 has-text-grey-dark has-text-centered mx-4 mt-4 mb-2">
                    {props.content.resume.summary.caption}
                </p>
            </Skeleton>
            <Skeleton isLoading={props.isLoading} mode="Text" height={24} className="my-4">
                <p className="is-size-6 has-text-grey-dark has-text-justified line-height-20">
                    {props.content.resume.summary.text}
                </p>
            </Skeleton>
            <Skeleton isLoading={props.isLoading} mode="Text" height={24} className="my-4">
                <p className="is-size-4 has-text-grey-dark has-text-centered mx-4 mt-4 mb-2">
                    {props.content.resume.achievements.caption}
                </p>
            </Skeleton>
            <div className="bulma-content has-text-grey-dark">
                <RenderList
                    isLoading={props.isLoading}
                    list={props.content.resume.achievements.list}
                    className="is-size-6 has-text-justified line-height-20"
                />
            </div>
            <Skeleton
                isLoading={props.isLoading}
                mode="Text"
                width={200}
                height={24}
                hasSkeletonCentered
                className="my-4"
            >
                <p className="is-size-4 has-text-grey-dark has-text-centered mx-4 mt-4 mb-2">
                    {props.content.resume.experience.caption}
                </p>
            </Skeleton>
            <RenderExperienceList {...props} />
            <Skeleton
                isLoading={props.isLoading}
                mode="Text"
                width={200}
                height={24}
                hasSkeletonCentered
                className="my-4"
            >
                <p className="is-size-4 has-text-grey-dark has-text-centered mx-4 mt-4 mb-2">
                    {props.content.resume.education.caption}
                </p>
            </Skeleton>
            <RenderEducationList {...props} />
            <Skeleton
                isLoading={props.isLoading}
                mode="Text"
                width={200}
                height={24}
                hasSkeletonCentered
                className="my-4"
            >
                <p className="is-size-4 has-text-grey-dark has-text-centered mx-4 mt-4 mb-2">
                    {props.content.resume.interests.caption}
                </p>
            </Skeleton>
            <RenderInterestsList {...props} />
        </div>
    </section>
);
