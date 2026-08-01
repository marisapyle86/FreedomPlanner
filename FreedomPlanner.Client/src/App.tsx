import { useEffect, useState } from 'react'
import DashboardCard from './components/DashboardCard'
import DashboardGrid from './components/DashboardGrid'
import DashboardHeader from './components/DashboardHeader'
import DashboardPage from './components/DashboardPage'
import './App.css'

type UserPlanResponse = {
  id: string
  name: string
  currency: string
  createdDate: string
}

type CashReserveSummaryResponse = {
  completionPercentage: number
  remainingAmount: number
  status: string
  recommendation: string
  insight: string | null
}

type DashboardViewModel = {
  generatedAtUtc: string
  assumptionVersion: string
  userPlan: UserPlanResponse | null
  cashReserve: CashReserveSummaryResponse | null
  recommendations: string[]
  insights: string[]
  mortgage: { title: string; description: string }
  investments: { title: string; description: string }
  freedomLadder: { title: string; description: string }
}

function App() {
  const [dashboard, setDashboard] = useState<DashboardViewModel | null>(null)
  const [error, setError] = useState('')

  const loadDashboard = async () => {
    const response = await fetch('http://localhost:5100/api/dashboard')

    if (!response.ok) {
      setError('Unable to load dashboard.')
      return
    }

    const payload = (await response.json()) as DashboardViewModel
    setDashboard(payload)
    setError('')
  }

  useEffect(() => {
    void loadDashboard()
  }, [])

  return (
    <DashboardPage>
      <DashboardHeader title="Freedom Planner Dashboard" subtitle="Overview of the current plan and reserve state." />

      {error ? <p className="error">{error}</p> : null}

      {dashboard ? (
        <DashboardGrid>
          <DashboardCard title="Dashboard Snapshot" subtitle={`Generated ${new Date(dashboard.generatedAtUtc).toLocaleString()} · Assumptions v${dashboard.assumptionVersion}`} />

          <DashboardCard title="User Plan">
            {dashboard.userPlan ? (
              <dl className="plan-details">
                <div>
                  <dt>Name</dt>
                  <dd>{dashboard.userPlan.name}</dd>
                </div>
                <div>
                  <dt>Currency</dt>
                  <dd>{dashboard.userPlan.currency}</dd>
                </div>
                <div>
                  <dt>Created</dt>
                  <dd>{new Date(dashboard.userPlan.createdDate).toLocaleString()}</dd>
                </div>
              </dl>
            ) : (
              <p>No plan has been created yet.</p>
            )}
          </DashboardCard>

          <DashboardCard title="Cash Reserve" subtitle="Calculated by the backend service">
            {dashboard.cashReserve ? (
              <>
                <div className="progress-row">
                  <span>Completion</span>
                  <strong>{dashboard.cashReserve.completionPercentage.toFixed(0)}%</strong>
                </div>
                <div className="progress-row">
                  <span>Remaining</span>
                  <strong>£{dashboard.cashReserve.remainingAmount.toFixed(0)}</strong>
                </div>
                <div className="progress-row">
                  <span>Status</span>
                  <strong>{dashboard.cashReserve.status}</strong>
                </div>
                {dashboard.cashReserve.recommendation ? (
                  <div className="progress-row">
                    <span>Recommendation</span>
                    <strong>{dashboard.cashReserve.recommendation}</strong>
                  </div>
                ) : null}
                {dashboard.cashReserve.insight ? (
                  <div className="progress-row">
                    <span>Insight</span>
                    <strong>{dashboard.cashReserve.insight}</strong>
                  </div>
                ) : null}
              </>
            ) : (
              <p>No reserve summary available.</p>
            )}
          </DashboardCard>

          <DashboardCard title="Recommendations">
            {dashboard.recommendations.length > 0 ? (
              <ul className="stack-list">
                {dashboard.recommendations.map((item) => (
                  <li key={item}>{item}</li>
                ))}
              </ul>
            ) : (
              <p>No recommendations yet.</p>
            )}
          </DashboardCard>

          <DashboardCard title="Insights">
            {dashboard.insights.length > 0 ? (
              <ul className="stack-list">
                {dashboard.insights.map((item) => (
                  <li key={item}>{item}</li>
                ))}
              </ul>
            ) : (
              <p>No insights yet.</p>
            )}
          </DashboardCard>

          <DashboardCard title={dashboard.mortgage.title} subtitle={dashboard.mortgage.description} />
          <DashboardCard title={dashboard.investments.title} subtitle={dashboard.investments.description} />
          <DashboardCard title={dashboard.freedomLadder.title} subtitle={dashboard.freedomLadder.description} />
        </DashboardGrid>
      ) : (
        <p>Loading dashboard…</p>
      )}
    </DashboardPage>
  )
}

export default App
