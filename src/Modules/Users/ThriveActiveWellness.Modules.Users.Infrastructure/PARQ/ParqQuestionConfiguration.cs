using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThriveActiveWellness.Modules.Users.Domain.PARQ;

namespace ThriveActiveWellness.Modules.Users.Infrastructure.PARQ;

public class ParqQuestionConfiguration : IEntityTypeConfiguration<ParqQuestion>
{
    public void Configure(EntityTypeBuilder<ParqQuestion> builder)
    {
        builder.ToTable("parq_questions");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();

        builder.Property(x => x.Question).IsRequired();
        
        builder.Property(x => x.ConditionType).IsRequired(false);

        builder.HasOne(x => x.ParentQuestion)
            .WithMany(x => x.ConditionalQuestions)
            .HasForeignKey(x => x.ParentQuestionId)
            .IsRequired(false);

        // Look into BoldSign for signing the generated PAR-Q
        
        builder.HasData(
            ParqQuestion.Create(1, "Has your doctor ever said that you have a heart condition OR high blood pressure?"),
            ParqQuestion.Create(2, "Do you feel pain in your chest at rest, during your daily activities of living, OR when you do physical activity?"),
            ParqQuestion.Create(3, "Do you lose balance because of dizziness OR have you lost consciousness in the last 12 months?"),
            ParqQuestion.Create(4, "Have you ever been diagnosed with another chronic medical condition (other than heart disease or high blood pressure)?"),
            ParqQuestion.Create(5, "Are you currently taking prescribed medications for a chronic medical condition?"),
            ParqQuestion.Create(6, "Do you currently have (or have had within the past 12 months) a bone, joint, or soft tissue problem that could be made worse by becoming more physically active?"),
            ParqQuestion.Create(7, "Has your doctor ever said that you should only do medically supervised physical activity?"),

            // Follow-up conditional questions
            ParqQuestion.Create(8, "Do you have Arthritis, Osteoporosis, or Back Problems?", 4, ConditionType.Yes),
            ParqQuestion.Create(9, "Do you have difficulty controlling your condition with medications or other physician-prescribed therapies?", 8, ConditionType.Yes),
            ParqQuestion.Create(10, "Do you have joint problems causing pain, a recent fracture, or fracture caused by osteoporosis or cancer?", 8, ConditionType.Yes),
            ParqQuestion.Create(11, "Have you had steroid injections or taken steroid tablets regularly for more than 3 months?", 8, ConditionType.Yes),

            ParqQuestion.Create(12, "Do you currently have Cancer of any kind?", 4, ConditionType.Yes),
            ParqQuestion.Create(13, "Does your cancer diagnosis include any of the following types: lung/bronchogenic, multiple myeloma, head, and/or neck?", 12, ConditionType.Yes),
            ParqQuestion.Create(14, "Are you currently receiving cancer therapy (such as chemotherapy or radiotherapy)?", 12, ConditionType.Yes),

            ParqQuestion.Create(15, "Do you have a Heart or Cardiovascular Condition?", 4, ConditionType.Yes),
            ParqQuestion.Create(16, "Do you have difficulty controlling your condition with medications or other physician-prescribed therapies?", 15, ConditionType.Yes),
            ParqQuestion.Create(17, "Do you have an irregular heartbeat that requires medical management?", 15, ConditionType.Yes),
            ParqQuestion.Create(18, "Do you have chronic heart failure?", 15, ConditionType.Yes),
            ParqQuestion.Create(19, "Do you have diagnosed coronary artery disease and have not participated in regular physical activity in the last 2 months?", 15, ConditionType.Yes),

            ParqQuestion.Create(20, "Do you have High Blood Pressure?", 4, ConditionType.Yes),
            ParqQuestion.Create(21, "Do you have difficulty controlling your condition with medications or other physician-prescribed therapies?", 20, ConditionType.Yes),
            ParqQuestion.Create(22, "Do you have a resting blood pressure equal to or greater than 160/90 mmHg with or without medication?", 20, ConditionType.Yes),

            ParqQuestion.Create(23, "Do you have any Metabolic Conditions?", 4, ConditionType.Yes),
            ParqQuestion.Create(24, "Do you often have difficulty controlling your blood sugar levels with foods, medications, or other physician-prescribed therapies?", 23, ConditionType.Yes),
            ParqQuestion.Create(25, "Do you often suffer from signs and symptoms of low blood sugar following exercise and/or during activities of daily living?", 23, ConditionType.Yes),
            ParqQuestion.Create(26, "Do you have any signs or symptoms of diabetes complications such as heart or vascular disease?", 23, ConditionType.Yes),
            ParqQuestion.Create(27, "Do you have other metabolic conditions such as pregnancy-related diabetes, chronic kidney disease, or liver problems?", 23, ConditionType.Yes),
            ParqQuestion.Create(28, "Are you planning to engage in unusually high intensity exercise in the near future?", 23, ConditionType.Yes),

            ParqQuestion.Create(29, "Do you have any Mental Health Problems or Learning Difficulties?", 4, ConditionType.Yes),
            ParqQuestion.Create(30, "Do you have difficulty controlling your condition with medications or other physician-prescribed therapies?", 29, ConditionType.Yes),
            ParqQuestion.Create(31, "Do you have Down Syndrome AND back problems affecting nerves or muscles?", 29, ConditionType.Yes),

            ParqQuestion.Create(32, "Do you have a Respiratory Disease?", 4, ConditionType.Yes),
            ParqQuestion.Create(33, "Do you have difficulty controlling your condition with medications or other physician-prescribed therapies?", 32, ConditionType.Yes),
            ParqQuestion.Create(34, "Has your doctor ever said your blood oxygen level is low at rest or during exercise and/or that you require supplemental oxygen therapy?", 32, ConditionType.Yes),
            ParqQuestion.Create(35, "If asthmatic, do you currently have symptoms of chest tightness, wheezing, laboured breathing, or consistent cough?", 32, ConditionType.Yes),
            ParqQuestion.Create(36, "Has your doctor ever said you have high blood pressure in the blood vessels of your lungs?", 32, ConditionType.Yes),

            ParqQuestion.Create(37, "Do you have a Spinal Cord Injury?", 4, ConditionType.Yes),
            ParqQuestion.Create(38, "Do you have difficulty controlling your condition with medications or other physician-prescribed therapies?", 37, ConditionType.Yes),
            ParqQuestion.Create(39, "Do you commonly exhibit low resting blood pressure significant enough to cause dizziness, light-headedness, and/or fainting?", 37, ConditionType.Yes),
            ParqQuestion.Create(40, "Has your physician indicated that you exhibit sudden bouts of high blood pressure (known as Autonomic Dysreflexia)?", 37, ConditionType.Yes),

            ParqQuestion.Create(41, "Have you had a Stroke?", 4, ConditionType.Yes),
            ParqQuestion.Create(42, "Do you have difficulty controlling your condition with medications or other physician-prescribed therapies?", 41, ConditionType.Yes),
            ParqQuestion.Create(43, "Do you have any impairment in walking or mobility?", 41, ConditionType.Yes),
            ParqQuestion.Create(44, "Have you experienced a stroke or impairment in nerves or muscles in the past 6 months?", 41, ConditionType.Yes),

            ParqQuestion.Create(45, "Do you have any other medical condition not listed above?", 4, ConditionType.Yes),
            ParqQuestion.Create(46, "Have you experienced a blackout, fainted, or lost consciousness as a result of a head injury within the last 12 months?", 45, ConditionType.Yes),
            ParqQuestion.Create(47, "Do you have a medical condition that is not listed (such as epilepsy, neurological conditions, kidney problems)?", 45, ConditionType.Yes),
            ParqQuestion.Create(48, "Do you currently live with two or more medical conditions?", 45, ConditionType.Yes)
        );
    }
}
