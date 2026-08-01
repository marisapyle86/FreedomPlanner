# Calculation Rules

**Version:** 1.0  
**Status:** Draft  
**Owner:** Product

---

# Purpose

This document defines the financial rules used throughout Freedom Planner.

It is the authoritative source for all calculations performed by the application.

The goal is to ensure calculations remain:

- Consistent
- Explainable
- Testable
- Independent of the user interface

Business rules should be defined here before implementation.

---

# Guiding Principles

## Money Buys Freedom

Every calculation should ultimately support better financial decisions rather than maximise wealth for its own sake.

---

## Explain Every Recommendation

If the application recommends an action, it must be able to explain:

- Why
- Expected benefit
- Any assumptions used

Recommendations must never appear arbitrary.

---

## Conservative Assumptions

Future projections should use realistic assumptions rather than optimistic ones.

Where uncertainty exists, prefer assumptions that slightly underestimate future outcomes.

---

## Deterministic Calculations

Given identical inputs and projection assumptions, the application must always produce identical outputs.

---

# Projection Settings

All future projections use configurable assumptions.

Version 1 defaults:

| Setting | Default |
|----------|---------|
| Inflation | 2.5% |
| Investment Growth | 5.5% |
| Property Growth | 2.0% |
| Savings Interest | User supplied |
| Safe Withdrawal Rate | 3.5% |

These values are configurable.

---

# Mortgage Calculations

## Loan Balance

Mortgage balance is supplied by the user and updated through repayments.

---

## Loan To Value (LTV)

Formula:

LTV = Mortgage Balance ÷ Property Value × 100

Displayed as a percentage.

---

## Mortgage Progress

Mortgage progress is based on percentage of original capital repaid.

Example:

Original Mortgage

£315,000

Current Balance

£295,000

Progress

£20,000 repaid

---

## Mortgage Completion

The application estimates:

- Remaining term
- Estimated completion date
- Total remaining interest

using the current repayment amount and mortgage assumptions.

---

## Mortgage Overpayments

Version 1 records overpayments but does not automatically optimise them.

Future versions will compare overpayments against investing.

---

# Cash Reserve

## Emergency Fund Target

Default target:

£10,000

Future versions may allow:

- Fixed amount
- Months of expenses

---

## Completion Percentage

Current Savings ÷ Target

Displayed as:

- Percentage
- Remaining amount
- Estimated completion date

---

# Investments

Investment accounts contain:

- Current value
- Monthly contribution
- Expected annual growth

---

## Growth Model

Version 1 assumes:

Monthly contributions

Monthly compounding

Constant annual growth

No tax adjustments

No fees

---

## Future Value

Projected using compound growth.

Actual investment performance will differ.

Future values are estimates only.

---

# Pension

Version 1 records:

- Current value
- Monthly contributions
- Employer contributions
- Retirement age

Projected values use Projection Settings.

Income estimates are outside Version 1 scope.

---

# Monthly Commitments

Monthly commitments represent recurring expenditure.

Examples:

- Mortgage
- Utilities
- Insurance
- Car finance
- Subscriptions

Commitments may optionally contain an end date.

Future surplus calculations should account for commitments ending.

---

# Available Monthly Capacity

Calculated as:

Monthly Income

minus

Monthly Commitments

minus

User-defined Living Costs

equals

Available Capacity

This represents money available for future decisions.

---

# Recommendations

Recommendations are generated automatically.

Each recommendation contains:

- Title
- Description
- Priority
- Category
- Reason
- Expected Benefit

Version 1 recommendations are prioritised rather than prescriptive.

Example:

High

Continue building Emergency Fund.

Reason:

Emergency fund below target.

---

# Recommendation Priority

Priority order:

1. Financial resilience
2. Debt management
3. Long-term investment
4. Optimisation
5. Nice-to-have improvements

The application should recommend the highest priority unfinished objective.

---

# Insights

Insights describe significant events.

Examples:

- Mortgage below £300,000
- Emergency Fund reached 50%
- Investment portfolio exceeded £25,000
- Freedom Ladder Level increased

Insights never recommend actions.

They only communicate progress.

---

# Overall Status

Overall Status provides a high-level summary.

Possible values:

🟢 On Track

🟡 Attention Needed

🔴 Off Track

Overall Status considers multiple dimensions:

- Emergency Fund
- Mortgage
- Investments
- Commitments
- Goals

No single financial metric determines overall status.

---

# Freedom Ladder

Freedom Ladder tracks overall progress towards financial independence.

It combines multiple financial milestones into one motivational framework.

Progression is determined by milestone completion rather than net worth alone.

The Freedom Ladder is both:

- Motivational
- Decision-supporting

Future versions may expand ladder levels.

---

# Opportunity Analysis

Opportunity Analysis compares alternative financial decisions.

Examples:

- Mortgage overpayment vs ISA
- Pension vs ISA
- Windfall allocation
- Additional monthly investment

Opportunity Analysis is user initiated.

It does not run automatically.

---

# Dashboard Generation

Dashboard calculations occur in this order:

1. Load User Plan
2. Validate Data
3. Calculate Financial Metrics
4. Run Projections
5. Generate Insights
6. Generate Recommendations
7. Determine Overall Status
8. Build Dashboard View Model

The frontend performs no financial calculations.

---

# Future Features

The following calculations are intentionally deferred:

- Freedom Date estimation
- Retirement bridge modelling
- Tax optimisation
- Dynamic withdrawal modelling
- Monte Carlo simulations
- Scenario comparison
- Inflation-adjusted spending forecasts

These will be introduced in later versions without changing the core calculation principles.

---

# Testing Philosophy

Every calculation defined in this document must be independently unit tested.

Tests should verify:

- Correct calculations
- Boundary conditions
- Invalid inputs
- Projection consistency

Business calculations must never rely on UI behaviour.

---

# Document Ownership

This document defines business rules.

Implementation details belong within the Architecture documentation.

If implementation and this document disagree, this document takes precedence.