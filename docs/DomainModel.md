# Domain Model

Version: 1.0

---

# Philosophy

Freedom Planner models a person's financial life.

It does not model bank accounts.

It models decisions.

Each entity should represent a real concept within the user's financial journey.

---

# User Plan

Represents the complete financial picture.

Contains:

- Mortgage
- Investments
- Pension
- Cash Reserve
- Monthly Commitments
- Goals
- Annual Reviews
- Recommendations
- Insights

There will only be one User Plan in Version 1.

UserPlan owns ongoing dashboard outputs such as Recommendations and Insights. Opportunity Analyses remain separate because they represent user-initiated decision exercises rather than continuous model outputs.

---

# Mortgage

Purpose

Represents the user's residential mortgage.

Properties

- Original Loan
- Current Balance
- Interest Rate
- Remaining Term
- Monthly Payment
- Maximum Overpayment
- Fixed Rate End Date
- Property Value

Calculated Values

- Loan To Value
- Interest Paid
- Capital Repaid
- Estimated Mortgage End Date

---

# Investment Account

Represents a single investment account.

Examples

- Trading212 ISA
- Trading212 GIA
- Vanguard ISA

Properties

- Name
- Account Type
- Current Balance
- Monthly Contribution
- Expected Return

Calculated Values

- Future Value
- Total Contributions
- Investment Gain

---

# Pension

Represents workplace and private pensions.

Properties

- Current Value
- Monthly Contribution
- Employer Contribution
- Retirement Age

Calculated Values

- Projected Value
- Annual Income Estimate

---

# Cash Reserve

Represents emergency savings.

Properties

- Current Balance
- Target Balance
- Interest Rate
- Monthly Contribution

Calculated Values

- Completion Date

---

# Monthly Commitment

Represents recurring financial commitments.

Examples

Mortgage

Car Finance

Insurance

Healthcare

Utilities

Properties

- Name
- Monthly Cost
- End Date (optional)

Calculated Values

- Remaining Lifetime Cost

---

# Goal

Represents a financial objective.

Examples

Emergency Fund

Mortgage Free

Semi Retirement

ISA £50k

ISA £100k

Properties

- Name
- Target Value
- Target Date
- Status

---

# Milestone

Represents important achievements.

Examples

Reach 85% LTV

Emergency Fund Complete

Mortgage Below £250k

Freedom Ladder Level 5

Properties

- Name
- Unlock Condition
- Date Achieved

---

# Freedom Ladder

Represents overall financial progression.

Properties

- Current Level
- Progress
- Next Level

Calculated Values

- Percentage Complete
- Next Achievement

---

# Annual Review

Represents one yearly financial review.

Each Annual Review is an immutable historical snapshot. Once created it should never be modified.

Contains:

- Salary
- Mortgage Balance
- House Value
- Investment Values
- Pension Value
- Cash Savings
- Monthly Capacity
- Notes
- Review Date

---

# Recommendation

Represents an automatically generated guidance output.

Recommendations are outputs of the financial model rather than user-entered data.

Examples

- Continue building your Emergency Fund.
- Consider increasing ISA contributions.
- You are currently on track. No action required.

Properties

- Title
- Description
- Priority
- Category
- Reason
- Estimated Benefit
- Date Generated

---

# Insight

Represents an informational change in the user's financial position.

Insights are informational only and do not recommend actions.

Examples

- Mortgage balance has fallen below £300,000.
- Freedom Date moved forward by 4 months.
- Freedom Ladder Level 3 unlocked.
- Pension exceeded £100,000.

Properties

- Title
- Description
- Date Generated

---

# Projection Settings

Represents configurable modelling assumptions used by the financial engine.

These settings keep assumptions separate from business logic and allow projections to change without modifying calculation behavior.

Examples

- Expected investment return
- Inflation
- House price growth
- Safe withdrawal rate
- Pension growth assumptions

Properties

- Expected Investment Return
- Inflation Rate
- House Price Growth
- Safe Withdrawal Rate
- Pension Growth Assumptions
- Other configurable model assumptions

---

# Opportunity Analysis

Represents a financial decision.

Examples

- Mortgage vs ISA
- Windfall
- Salary Increase

Properties

- Scenario
- Recommendation
- Reasoning
- Estimated Impact
- Confidence

---

# Dashboard

The Dashboard is not a database entity.

It is a projection built from the other domain objects.

It should answer:

- Am I on track?
- What changed?
- What should I do next?
- What milestone is next?

The Dashboard should consume Insights and Recommendations rather than containing its own business logic.

---

# Relationships

User Plan

├── Mortgage

├── Cash Reserve

├── Pension

├── Investment Accounts

├── Monthly Commitments

├── Goals

├── Annual Reviews

├── Freedom Ladder

├── Recommendations

├── Insights

└── Opportunity Analysis

---

# Domain Rule

Business calculations belong in the domain.

The UI should never contain financial calculations.

The UI only displays the current state of the domain.