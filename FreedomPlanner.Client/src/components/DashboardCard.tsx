import type { ReactNode } from 'react'

type DashboardCardProps = {
  title: string
  subtitle?: string
  children?: ReactNode
}

function DashboardCard({ title, subtitle, children }: DashboardCardProps) {
  return (
    <section className="dashboard-card">
      <header className="dashboard-card-header">
        <h2>{title}</h2>
        {subtitle ? <p>{subtitle}</p> : null}
      </header>
      {children ? <div className="dashboard-card-body">{children}</div> : null}
    </section>
  )
}

export default DashboardCard
